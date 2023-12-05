using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected D_DodgeState stateData;
    private Enemy2 enemy;


    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;


    //dodge 
    private bool canJumpBack;
    private bool canJumpFront;
    private Transform jumpBack;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected LayerMask WhatIsGround;



    public DodgeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isGrounded = core.CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;

        jumpBack = GameObject.Find("LandZoneBack").transform;
        isDetectingLedge = core.CollisionSenses.LedgeVertical;
        isDetectingWall = core.CollisionSenses.WallFront;
        WhatIsGround = core.CollisionSenses.WhatIsGround;


        canJumpFront = (isDetectingLedge && !isDetectingWall);
        canJumpBack = Physics2D.OverlapCircle(jumpBack.position, 0.2f, WhatIsGround);

        Debug.Log(canJumpFront);
        if (canJumpBack)
        {
            //jump Back
            core.Movement.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -core.Movement.FacingDirection);
        }
        else if (canJumpFront)
        {
            //jump Fowards
            core.Movement.SetVelocity(stateData.dodgeSpeed*1.2f, stateData.dodgeAngle, core.Movement.FacingDirection);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.dodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}