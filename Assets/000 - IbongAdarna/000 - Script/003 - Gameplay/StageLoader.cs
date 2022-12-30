using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLoader : MonoBehaviour
{
    [SerializeField] private PlayerStateController playerStateController;

    private void Awake()
    {
        GameManager.Instance.sceneController.AddActionLoadinList(playerStateController.InitializeAnimationStatePlayer());

        GameManager.Instance.sceneController.ActionPass = true;
    }
}
