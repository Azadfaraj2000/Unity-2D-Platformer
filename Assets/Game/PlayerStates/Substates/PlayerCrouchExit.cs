using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchExit : PlayerGroundedState
{
    public PlayerCrouchExit(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    public override void Enter()
    {

    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();
        if (!isExitingState)
        {
            if (xInput != 0)
            {
        
                stateMachine.ChangeState(player.MoveState);

            }
            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
