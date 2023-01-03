using Cinemachine;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerDirection : MonoBehaviour
{
    public int CurrentDirection { get; private set; } = 1;

    //  ===================================

    [SerializeField] private GameplayController controller;
    [SerializeField] private Transform playerTF;

    [Header("CAMERA EFFECT")]
    [SerializeField] private CinemachineCameraOffset vcamOffset;
    [SerializeField] private float animationSpeed;

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private float time;

    //  ===================================

    private void Awake()
    {
        CurrentDirection = 1;
    }

    private void Update()
    {
        CameraPlacement();
    }

    public void FlipPlayer(int direction)
    {
        if (direction != 0 && direction != CurrentDirection) Flip();
    }

    private void Flip()
    {
        time = 0f;
        CurrentDirection *= -1;

        playerTF.rotation = Quaternion.Euler(new Vector3(0, 85 * CurrentDirection, 0f));
    }

    private void CameraPlacement()
    {
        if (CurrentDirection == 1)
        {
            if (vcamOffset.m_Offset.x < 8.83f)
            {
                time += Time.deltaTime / animationSpeed;
                if (time <= 1.0f)
                    vcamOffset.m_Offset.x = Mathf.Lerp(vcamOffset.m_Offset.x, 8.83f, time);
            }
        }
        else if (CurrentDirection == -1)
        {
            if (vcamOffset.m_Offset.x > -8.83f)
            {
                time += Time.deltaTime / animationSpeed;
                if (time <= 1.0f)
                    vcamOffset.m_Offset.x = Mathf.Lerp(vcamOffset.m_Offset.x, -8.83f, time);
            }
        }
    }
}
