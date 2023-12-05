using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovementWeapon : WeaponComponent<MovementData, AttackMovement>
{

    private Movement coreMovement;

    private Movement CoreMovement =>
        coreMovement ? coreMovement : Core.GetCoreComponent(ref coreMovement);


    private void HandleStartMovement()
    {

        Core.Movement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, Core.Movement.FacingDirection);
    }

    private void HandleStopMovement()
    {
        Core.Movement.SetVelocityZero();

        
    }


    protected override void Start()
    {
        base.Start();

        eventHandler.OnStartMovement += HandleStartMovement;
        eventHandler.OnStopMovement += HandleStopMovement;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();

        eventHandler.OnStartMovement -= HandleStartMovement;
        eventHandler.OnStopMovement -= HandleStopMovement;
    }


}
