using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        if (!isExitingState)
        {

            core.Movement.SetVelocityX(0);
            core.Movement.SetVelocityY(-playerData.wallSlideVelcoity);


        }


    }
}
