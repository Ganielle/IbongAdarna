using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public bool LeftTurn { get; private set; }
    public bool RightTurn { get; private set; }
    public int MovementDirection { get; private set; }

    //  =================================

    private GameController gameController;

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
        gameController.Disable();
    }

    private void Start()
    {
        gameController.Game.LeftMovement.started += _ => LeftMovementStart();
        gameController.Game.LeftMovement.canceled += _ => LeftMovementCancel();

        gameController.Game.RightMovement.started += _ => RightMovementStart();
        gameController.Game.RightMovement.canceled += _ => RightMovementCancel();
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
}
