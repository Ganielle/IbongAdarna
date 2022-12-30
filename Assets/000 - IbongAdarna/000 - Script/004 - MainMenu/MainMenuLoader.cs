using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoader : MonoBehaviour
{
    [SerializeField] private MainMenuController mainMenuController;

    private void Awake()
    {
        GameManager.Instance.sceneController.AddActionLoadinList(StageSelectInitialize());
        GameManager.Instance.sceneController.ActionPass = true;
    }

    IEnumerator StageSelectInitialize()
    {
        mainMenuController.ResetStageSelectMenu();
        yield return null;
    }
}
