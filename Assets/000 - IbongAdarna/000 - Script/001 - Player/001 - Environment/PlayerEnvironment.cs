using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnvironment : MonoBehaviour
{
    public Vector2 CurrentVelocity 
    {
        get => currentVelocity; 
        private set
        {
            currentVelocity = value;
        }
    }

    public Vector2 Workspace
    {
        get => workspace;
        private set
        {
            workspace = value;
        }
    }

    public int CurrentDirection { get; private set; }

    //  ================================

    [SerializeField] private PlayerMovementData movementData;
    [SerializeField] private Rigidbody2D playerRB;

    //  ================================

    [ReadOnly] [SerializeField] private Vector2 currentVelocity;
    [ReadOnly] [SerializeField] private Vector2 workspace;

    //  ================================

    public void CurrentVelocitySet() => CurrentVelocity = playerRB.velocity;
    public void SetWorkspace(float x, float y) => workspace.Set(x, y);
    public void SetVelocityZero()
    {
        playerRB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    public void SetVelocityX(float valueX)
    {
        workspace.Set(valueX, CurrentVelocity.y);
        playerRB.velocity = workspace;
        currentVelocity = workspace;
    }
    public void SetVelocityY(float value)
    {
        workspace.Set(currentVelocity.x, value);
        playerRB.velocity = workspace;
        currentVelocity = workspace;
    }
}
