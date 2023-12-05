using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPos;
    public bool CanJumpAnimationFinished = false;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        stateMachine.ChangeState(player.WallSlideState);

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        Debug.Log("Enter wallgrab state");
        holdPos = player.transform.position;
        base.Enter();
        HoldPosition();

    }


    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HoldPosition();
        
        if (jumpInput == true)
        {
            HoldPosition();
            //stateMachine.ChangeState(player.WallJumpState);
        }

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void HoldPosition()
    {
        player.transform.position = holdPos;
        core.Movement.SetVelocityZero();
    }

}
