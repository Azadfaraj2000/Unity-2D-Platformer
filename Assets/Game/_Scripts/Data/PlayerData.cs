using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{


    [Header("Move State")]
    public float movementVelocity = 5f;

    [Header("Jump State")]
    public int amountOfJumps = 1;
    public float jumpVelocity = 10f;
    [Header("In Air State")]
    public float coyoteTime = 0.2f;

    [Header("Wall Slide State")]
    public float wallSlideVelcoity;

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;
    public Vector2 hopStartOffset;
    public Vector2 hopStopOffset;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    public float wallCheckDistance =0.5f;
    [Header("Wall Jump Stat")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.2f;
    public Vector2 wallJumpAngle = new Vector2(1,1);


    [Header("Dash State")]
    public float dashCoolDown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.1f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float DashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.5f;


    [Header("Roll State")]
    public float rollCoolDown = 0.5f;
    public float rollMaxHoldTime = 1f;
    public float rollHoldTimeScale = 0.1f;
    public float rollTime = 0.2f;
    public float rollVelocity = 30f;
    public float rollDrag = 10f;
    public float RollEndYMultiplier = 0.2f;
    public float rollDistBetweenAfterImages = 0.5f;

}