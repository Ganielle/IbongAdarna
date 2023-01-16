using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

    //  ================================

    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private PlayerMovementData movementData;
    [SerializeField] private PlayerDirection direction;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private PhysicsMaterial2D inAirMat;
    [SerializeField] private PlayerGameplay gameplay;

    //  ================================

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private Vector2 currentVelocity;
    [ReadOnly] [SerializeField] private Vector2 workspace;
    [ReadOnly] [SerializeField] private bool triggerCollapseFloor;

    //  ================================

    Action stopCollapse;

    Collider2D wallCheck;
    bool finalWallCheck;

    //  ================================

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CollapsableFloor") && Grounded && !triggerCollapseFloor)
        {
            triggerCollapseFloor = true;
            collision.gameObject.GetComponent<CollapsingPlatform>().StartToCollapse();
            stopCollapse = collision.gameObject.GetComponent<CollapsingPlatform>().StopCollapseOnExit;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CollapsableFloor") && !Grounded)
        {
            triggerCollapseFloor = false;
            stopCollapse?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("respawnpoints"))
        {
            gameplayManager.RespawnPosition = transform.position;
        }

        if (collision.CompareTag("death"))
        {
            transform.position = gameplayManager.RespawnPosition;
            gameplay.HealthChanger(true);
        }
    }

    public void CurrentVelocitySet() => CurrentVelocity = playerRB.velocity;
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
    public void SetVelocitJumpX(float valueX, float valueY)
    {
        workspace.Set(valueX, valueY);
        playerRB.velocity = workspace;
        currentVelocity = workspace;
    }
    public void SetVelocityY(float value)
    {
        workspace.Set(currentVelocity.x, value);
        playerRB.velocity = workspace;
        currentVelocity = workspace;
    }

    public void SetInAirMat() => playerCollider.sharedMaterial = inAirMat;

    public void RemoveInAirMat() => playerCollider.sharedMaterial = null;

    public bool Grounded
    {
        get => Physics2D.OverlapCircle(transform.position, movementData.GroundRadius, movementData.GroundLayer);
    }

    public bool TouchWall
    {
        get
        {
            wallCheck = Physics2D.OverlapBox(transform.position + new Vector3(movementData.WallFrontCheckPos.x * direction.CurrentDirection,
            movementData.WallFrontCheckPos.y, 0f), movementData.WallFrontRadius, LayerMask.NameToLayer("wall"));

            if (wallCheck != null)
            {
                finalWallCheck = wallCheck.gameObject.layer == 7;

                return finalWallCheck;
            }
            else return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, movementData.GroundRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + new Vector3(movementData.WallFrontCheckPos.x * direction.CurrentDirection,
            movementData.WallFrontCheckPos.y, 0f), movementData.WallFrontRadius);
    }
}
