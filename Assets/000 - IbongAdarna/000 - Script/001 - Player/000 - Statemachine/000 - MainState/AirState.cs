using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : PlayerStatemachine
{
    public AirState(PlayerStateChanger changer, PlayerMovementData movementData, 
        PlayerActiveData activeData, PlayerStateController controller, 
        GameplayController gameController, PlayerEnvironment environment, PlayerDirection direction, 
        string animationName) : base(changer, movementData, activeData, controller, gameController, environment,
            direction, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Environment.SetInAirMat();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        AnimationChange();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        MoveInAir();
    }

    private void MoveInAir()
    {
        if (!Environment.TouchWall)
        {
            if (!GameControl.LeftTurn && !GameControl.RightTurn) return;

            Direction.FlipPlayer(GameControl.MovementDirection);
            Environment.SetVelocitJumpX(MovementData.AirMovementSpeed * Direction.CurrentDirection,
                Environment.CurrentVelocity.y);
        }
    }

    private void AnimationChange()
    {
        if (!AnimationExiting)
        {
            if (Environment.Grounded)
            {
                Changer.ChangeState(Controller.Idle);
            }
        }
    }
}
