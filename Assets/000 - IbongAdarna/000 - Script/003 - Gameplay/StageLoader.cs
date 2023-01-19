using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLoader : MonoBehaviour
{
    [SerializeField] private PlayerStateController playerStateController;
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private CutsceneManager cutsceneManager;

    private void Awake()
    {
        GameManager.Instance.sceneController.AddActionLoadinList(gameplayManager.ChangeBG());
        GameManager.Instance.sceneController.AddActionLoadinList(playerStateController.InitializeAnimationStatePlayer());
        GameManager.Instance.sceneController.AddActionLoadinList(cutsceneManager.PlayCutscene());

        GameManager.Instance.sceneController.ActionPass = true;
    }
}
