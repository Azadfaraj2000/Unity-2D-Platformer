using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountofJumpsLeft;
    
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
        amountofJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        core.Movement.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountofJumpsLeft--;
    }
    public bool CanJump()
    {
        if (amountofJumpsLeft > 0)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
    public void ResetAmountOfJumpsLeft() => amountofJumpsLeft = playerData.amountOfJumps;
    public void DecreaseAmountOfJumpsLeft() => amountofJumpsLeft--;

    public override void LogicUpdate()
    {

        base.LogicUpdate();
    }
}
