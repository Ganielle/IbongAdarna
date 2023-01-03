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

    //  ==============================================

    public PlayerStateChanger Changer { get; set; }
    public IdleState Idle { get; set; }
    public MoveState Move { get; set; }
    public AirState InAir { get; set; }
    public JumpState Jump { get; set; }

    //  ==============================================

    public IEnumerator InitializeAnimationStatePlayer()
    {
        Changer = new PlayerStateChanger();
        Idle = new IdleState(Changer, MovementData, ActiveData, this, Controller, Environment, Direction, "idle");
        Move = new MoveState(Changer, MovementData, ActiveData, this, Controller, Environment, Direction, "run");
        InAir = new AirState(Changer, MovementData, ActiveData, this, Controller, Environment, Direction, "inAir");
        Jump = new JumpState(Changer, MovementData, ActiveData, this, Controller, Environment, Direction, "inAir");

        Changer.Initialize(Idle);
        Direction.FlipPlayer(1);
        yield return null;
    }

    private void Update()
    {
        if (Changer != null)
            Changer.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        Environment.CurrentVelocitySet();
        if (Changer != null)
            Changer.CurrentState.PhysicsUpdate();
    }
}
