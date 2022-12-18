using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatemachine
{
    public PlayerStateChanger Changer { get; private set; }
    public PlayerMovementData MovementData { get; private set; }
    public PlayerActiveData ActiveData { get; private set; }
    public PlayerStateController Controller { get; private set; }

    //  ======================================

    public bool AnimationFinished { get; private set; }
    public bool AnimationExiting { get; private set; }
    public string AnimationName { get; private set; }

    //  ======================================
    
    public PlayerStatemachine(PlayerStateChanger changer, PlayerMovementData movementData, PlayerActiveData activeData,
        PlayerStateController controller, string animationName)
    {
        Changer = changer;
        MovementData = movementData;
        ActiveData = activeData;
        AnimationName = animationName;
        Controller = controller;
    }

    public virtual void Enter()
    {
        DoChecks();

        Controller.playerAnimator.SetBool(AnimationName, true);

        AnimationFinished = false;
        AnimationExiting = false;
    }

    public virtual void Exit()
    {
        Controller.playerAnimator.SetBool(AnimationName, false);

        AnimationExiting = true;
    }

    public virtual void TriggerEnter() { }

    public virtual void TriggerStay() { }

    public virtual void TriggerExit() { }

    public virtual void CollideEnter() { }

    public virtual void CollideStay() { }

    public virtual void CollideExit() { }

    public virtual void LogicUpdate()
    {
        DoChecks();
    }
    public virtual void PhysicsUpdate() { }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => AnimationFinished = true;
}
