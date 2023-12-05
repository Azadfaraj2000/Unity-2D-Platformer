using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerAbilityState
{
    public bool CanRoll { get; private set; }

    private bool rollInputStop;

    private float lastRollTime;

    private Vector2 rollDirection;
   
    private Vector2 lastAIPos;

    public PlayerDodgeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        CanRoll = false;


        player.InputHandler.UseRollInput();

        rollDirection = Vector2.right * core.Movement.FacingDirection;


        startTime = Time.time;
        core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(rollDirection.x));
 
        core.Movement.SetVelocity(playerData.rollVelocity, rollDirection);



    }

    public override void Exit()
    {
        base.Exit();

        if (core.Movement.CurrentVelocity.y > 0)
        {
            core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.RollEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {

            stateMachine.ChangeState(player.IdleState);
            player.RB.drag = 0f;
            isAbilityDone = true;
            lastRollTime = Time.time;
        }

    }




    public bool CheckIfCanRoll()
    {
        return true;
        //return CanRoll && Time.time >= lastRollTime + playerData.rollCoolDown;
    }



    public void ResetCanRoll() => CanRoll = true;


}