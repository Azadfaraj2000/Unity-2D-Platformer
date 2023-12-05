using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerInAirState : PlayerState
{
    //Input
    private int xInput;
    public bool jumpInput;
    private bool dashInput;
    //Check
    private bool isGrounded;
    private bool coyoteTime;
    private bool wallJumpcoyoteTime;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private float startWallTimeCoyoteTime = 0.1f;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    public bool isWallJump = false;
    private bool isTouchingLedge;
    private bool isTouchingHop;
    
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = core.CollisionSenses.Ground;
        isTouchingWall = core.CollisionSenses.WallFront;
        isTouchingWallBack = core.CollisionSenses.WallBack;
        isTouchingLedge = core.CollisionSenses.LedgeHorizontal;



        if (isTouchingWall && !isTouchingLedge)
        {
            //Debug.Log("isgrounded=" + isGrounded);
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
        if (!wallJumpcoyoteTime && isTouchingWallBack &&isTouchingWall&&(!oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }

        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;

        dashInput = player.InputHandler.DashInput;


        //attacks
        if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }


        //land
        else if (isGrounded && core.Movement.CurrentVelocity.y < 0.1f)
        {
            stateMachine.ChangeState(player.LandState);
        }

        //ledge
        else if(isTouchingWall && !isTouchingLedge  &&!isGrounded)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }

        /*
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpcoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = core.CollisionSenses.WallFront;
            
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        */


        
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }

        //wall grab
        else if (isTouchingWall && player.WallJumpState.isWallJumping)
        {
            player.WallJumpState.isWallJumping = false;

            stateMachine.ChangeState(player.WallGrabState);

        }

        else if ((isTouchingWall && xInput == core.Movement.FacingDirection && core.Movement.CurrentVelocity.y <= 0))
        {
            player.WallJumpState.isWallJumping = false;
            stateMachine.ChangeState(player.WallGrabState);
        }


        //dash
        else if(dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }

        else
        {
            core.Movement.CheckIfShouldFlip(xInput);
            core.Movement.SetVelocityX(playerData.movementVelocity*xInput);
            player.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
    public void StartCoyoteTime() => coyoteTime = true;
    public void StartWallJumpCoyoteTime()
    {
        wallJumpcoyoteTime = true;
        startWallTimeCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpcoyoteTime = false;
    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpcoyoteTime && Time.time > startWallTimeCoyoteTime + playerData.coyoteTime)
        {
            wallJumpcoyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }
}
