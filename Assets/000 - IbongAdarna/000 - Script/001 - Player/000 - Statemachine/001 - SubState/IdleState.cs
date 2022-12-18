using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundState
{
    public IdleState(PlayerStateChanger changer, PlayerMovementData movementData, PlayerActiveData activeData, 
        PlayerStateController controller, string animationName) : 
        base(changer, movementData, activeData, controller, animationName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    private void AnimationChanger()
    {
        if (!AnimationExiting)
        {

        }
    }
}
