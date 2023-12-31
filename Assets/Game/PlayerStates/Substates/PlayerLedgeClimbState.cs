﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workspace;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;
    private int xInput;
    private int yInput;

    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {

    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("ClimbLedge", false);
    }

    public override void AnimationTrigger()
    {
        
        base.AnimationTrigger();
        isHanging = true;
    }

    public override void Enter()
    {
        
        base.Enter();
        core.Movement.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (core.Movement.FacingDirection * playerData.startOffset.x),cornerPos.y -playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (core.Movement.FacingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);
        player.transform.position = startPos;
        isHanging = true;
    }

    public override void Exit()
    {
        base.Exit();
        isHanging = false;
        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;
            jumpInput = player.InputHandler.JumpInput;
            core.Movement.SetVelocityZero();
            player.transform.position = startPos;


            if (!isClimbing)
            {
                isClimbing = true;
                player.Anim.SetBool("ClimbLedge", true);
            }

            /*
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.InAirState);
            }
            */
            //else if (jumpInput && !isClimbing)
            //{
              //  player.WallJumpState.DetermineWallJumpDirection(true);
                //stateMachine.ChangeState(player.WallJumpState);
            //}
        }
        
 
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;
    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(core.CollisionSenses.WallCheck.position, Vector2.right * core.Movement.FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * core.Movement.FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(core.CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(workspace), Vector2.down, core.CollisionSenses.LedgeCheckHorizontal.position.y - core.CollisionSenses.WallCheck.position.y + 0.015f, playerData.whatIsGround);
        float yDist = yHit.distance;

        workspace.Set(core.CollisionSenses.WallCheck.position.x + (xDist * core.Movement.FacingDirection), core.CollisionSenses.LedgeCheckHorizontal.position.y - yDist);
        return workspace;
    }
}

