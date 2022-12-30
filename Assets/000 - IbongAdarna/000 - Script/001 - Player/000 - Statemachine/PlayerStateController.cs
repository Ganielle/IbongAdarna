using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementData MovementData { get; private set; }
    [field: SerializeField] public PlayerActiveData ActiveData { get; private set; }
    [field: SerializeField] public PlayerEnvironment Environment { get; private set; }
    [field: SerializeField] public PlayerDirection Direction { get; private set; }
    [field: SerializeField] public GameplayController Controller { get; private set; }
    [field: SerializeField] public Animator PlayerAnimator { get; private set; }


    [field: Header("DEBUGGER")]
    [field: ReadOnly] [field: SerializeField] public bool Grounded;
    //[field: ReadOnly] [field: SerializeField] public bool 

    //  ==============================================

    public PlayerStateChanger Changer { get; set; }
    public IdleState Idle { get; set; }
    public MoveState Move { get; set; }

    //  ==============================================

    public IEnumerator InitializeAnimationStatePlayer()
    {
        Changer = new PlayerStateChanger();
        Idle = new IdleState(Changer, MovementData, ActiveData, this, Controller, Environment, Direction, "idle");
        Move = new MoveState(Changer, MovementData, ActiveData, this, Controller, Environment, Direction, "run");

        Changer.Initialize(Idle);
        yield return null;
    }

    private void Update()
    {
        if (Changer != null)
            Changer.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        if (Changer != null)
            Changer.CurrentState.PhysicsUpdate();
    }
}
