using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundState
{
    public MoveState(PlayerStateChanger changer, PlayerMovementData movementData, 
        PlayerActiveData activeData, PlayerStateController controller, GameplayController gameController, PlayerEnvironment environment,
        PlayerDirection direction, string animationName) :
        base(changer, movementData, activeData, controller, gameController, environment, direction, animationName)
    {
    }

    public override void LogicUpdate()
    {
        ChangeAnimation();

        Direction.FlipPlayer(GameControl.MovementDirection);
    }

    public override void PhysicsUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Environment.SetVelocityX(MovementData.GroundMovementSpeed * Direction.CurrentDirection);
    }

    private void ChangeAnimation()
    {
        if (!AnimationExiting)
        {
            if (GameControl.Jump)
            {
                Changer.ChangeState(Controller.Jump);
            }
            if (!GameControl.LeftTurn && !GameControl.RightTurn)
            {
                Changer.ChangeState(Controller.Idle);
            }
        }
    }
}
