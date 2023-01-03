using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : GroundState
{
    public bool Jumping { get; private set; }

    public JumpState(PlayerStateChanger changer, PlayerMovementData movementData,
        PlayerActiveData activeData, PlayerStateController controller, GameplayController gameController,
        PlayerEnvironment environment, PlayerDirection direction, string animationName) : base(changer, 
            movementData, activeData, controller, gameController, environment, direction, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Jumping = true;

        if (Environment.Grounded)
            Environment.SetVelocityY(MovementData.JumpStrength);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (!AnimationExiting)
        {
            if (Environment.Grounded )
            {
                Changer.ChangeState(Controller.Idle);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        JumpHighLow();
        MoveInAir();
    }

    private void MoveInAir()
    {
        Debug.Log(Environment.TouchWall);
        if (Environment.TouchWall) return;

        if (!GameControl.LeftTurn && !GameControl.RightTurn) return;

        Direction.FlipPlayer(GameControl.MovementDirection);
        Environment.SetVelocitJumpX(MovementData.AirMovementSpeed * Direction.CurrentDirection,
            Environment.CurrentVelocity.y);
    }

    private void JumpHighLow()
    {
        if (GameControl.JumpStop)
        {
            Environment.SetVelocityY(Environment.CurrentVelocity.y * MovementData.JumpHeightMultiplier);
            GameControl.JumpTurnOff();
            Jumping = false;
        }

        else if (!GameControl.Jump) GameControl.JumpTurnOff();
    }
}
