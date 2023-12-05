using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct WeaponAttackDetails
{
    public string attackName;
    public float movementSpeed;
    public float damageAmount;
    //knockback
    public float knockbackStrength;
    public Vector2 knockbackAngle;
}