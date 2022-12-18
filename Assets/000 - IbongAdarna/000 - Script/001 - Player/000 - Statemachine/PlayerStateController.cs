using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public Animator playerAnimator;

    [field: Header("DEBUGGER")]
    [field: ReadOnly] [field: SerializeField] public bool Grounded;
    //[field: ReadOnly] [field: SerializeField] public bool 

    //  ==============================================

    public PlayerStateChanger Changer { get; private set; }

    //  ==============================================

    private void Awake()
    {
        
    }

    IEnumerator InitializeAnimationStatePlayer()
    {
        Changer = new PlayerStateChanger();
        yield return null;
    }

    private void Update()
    {
        Changer.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        Changer.CurrentState.PhysicsUpdate();
    }
}
