using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : PlayerStatemachine
{
    public GroundState(PlayerStateChanger changer, PlayerMovementData movementData,
        PlayerActiveData activeData, PlayerStateController controller, GameplayController gameController, PlayerEnvironment environment,
        PlayerDirection direction, string animationName) :
        base(changer, movementData, activeData, controller, gameController, environment, direction, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Environment.RemoveInAirMat();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // AIR STATE HERE
        if (!AnimationExiting)
        {
            if (!Environment.Grounded && !GameControl.Jump && !Controller.Jump.Jumping)
            {
                Changer.ChangeState(Controller.InAir);
            }
        }
    }
}
