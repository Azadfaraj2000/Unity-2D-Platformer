using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : WeaponComponent<KnockBackData, AttackKnockBack>
{
    private ActionHitBox hitBox;

    private Movement movement;

    private void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IKnockbackable knockBackable))
            {
                knockBackable.Knockback(currentAttackData.Angle, currentAttackData.Strength, movement.FacingDirection);
            }
        }
    }

    protected override void Start()
    {
        base.Start();

        hitBox = GetComponent<ActionHitBox>();

        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;

        movement = Core.GetCoreComponent<Movement>();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }
}
