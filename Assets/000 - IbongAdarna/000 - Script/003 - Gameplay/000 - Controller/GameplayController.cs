using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public bool LeftTurn { get; private set; }
    public bool RightTurn { get; private set; }
    public int MovementDirection { get; private set; }
    public bool Jump { get; private set; }
    public bool JumpStop { get; private set; }

    //  =================================

    private GameController gameController;

    [Header("OPTIONS")]
    [SerializeField] private float jumpHoldTime;

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private float currentJumpTime;

    //  =================================

    private void Awake()
    {
        gameController = new GameController();
    }

    private void OnEnable()
    {
        gameController.Enable();
    }

    private void OnDisable()
    {
        gameController.Game.LeftMovement.started -= _ => LeftMovementStart();
        gameController.Game.LeftMovement.canceled -= _ => LeftMovementCancel();

        gameController.Game.RightMovement.started -= _ => RightMovementStart();
        gameController.Game.RightMovement.canceled -= _ => RightMovementCancel();

        gameController.Game.Jump.started -= _ => JumpStart();
        gameController.Game.Jump.canceled -= _ => JumpCancel();

        gameController.Disable();
    }

    private void Start()
    {
        gameController.Game.LeftMovement.started += _ => LeftMovementStart();
        gameController.Game.LeftMovement.canceled += _ => LeftMovementCancel();

        gameController.Game.RightMovement.started += _ => RightMovementStart();
        gameController.Game.RightMovement.canceled += _ => RightMovementCancel();

        gameController.Game.Jump.started += _ => JumpStart();
        gameController.Game.Jump.canceled += _ => JumpCancel();
    }

    private void Update()
    {
        JumpInputHoldTime();
    }

    private void LeftMovementStart()
    {
        MovementDirection = -1;
        LeftTurn = true;
    }

    private void LeftMovementCancel()
    {
        LeftTurn = false;
    }

    private void RightMovementStart()
    {
        MovementDirection = 1;
        RightTurn = true;
    }

    private void RightMovementCancel()
    {
        RightTurn = false;
    }


    private void JumpStart()
    {
        Jump = true;
        JumpStop = false;
        currentJumpTime = Time.time;
    }

    private void JumpCancel()
    {
        Jump = false;
        JumpStop = true;
    }

    private void JumpInputHoldTime()
    {
        if (Time.time >= currentJumpTime + jumpHoldTime)
        {
            Jump = false;
            currentJumpTime = 0f;
        }
    }

    public void JumpTurnOff()
    {
        Jump = false;
    }
}
