using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundState
{
    public IdleState(PlayerStateChanger changer, PlayerMovementData movementData, PlayerActiveData activeData, 
        PlayerStateController controller, GameplayController gameController, PlayerEnvironment environment,
        PlayerDirection direction, string animationName) :
        base(changer, movementData, activeData, controller, gameController, environment, direction, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Environment.SetVelocityZero();
    }

    public override void LogicUpdate()
    {
        base.Enter();

        AnimationChanger();
    }

    private void AnimationChanger()
    {
        if (!AnimationExiting)
        {
            if (GameControl.LeftTurn || GameControl.RightTurn)
            {
                Direction.FlipPlayer(GameControl.MovementDirection);
                Changer.ChangeState(Controller.Move);
            }
        }
    }
}
