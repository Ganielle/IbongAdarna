using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private PlayerGameplay playerGameplay;

    [Header("DIALOGUE")]
    [SerializeField] private GameObject dialogueObj;

    [Header("STAGE COMPLETE")]
    [SerializeField] private CanvasGroup stageCompleteCG;
    [SerializeField] private CanvasGroup stageCompleteButtonsCG;

    [Header("HEALTH")]
    [SerializeField] private List<GameObject> healthIndicatorList;

    [Header("GAME OVER")]
    [SerializeField] private CanvasGroup gameOverCG;
    [SerializeField] private CanvasGroup gameOverButtonsCG;

    [field: Header("DEBUGGER")]
    [field: ReadOnly] [field: SerializeField] public Vector3 RespawnPosition { get; set; }

    private void OnEnable()
    {
        playerGameplay.OnHealthChange += HealthChange;
    }

    private void OnDisable()
    {
        playerGameplay.OnHealthChange -= HealthChange;
    }

    private void HealthChange(object sender, EventArgs e)
    {
        ChangeHealth();
    }

    private void ChangeHealth()
    {
        switch (playerGameplay.Health)
        {
            case 1:
                healthIndicatorList[2].SetActive(false);
                healthIndicatorList[1].SetActive(false);
                healthIndicatorList[0].SetActive(true);
                break;
            case 2:
                healthIndicatorList[2].SetActive(false);
                healthIndicatorList[1].SetActive(true);
                healthIndicatorList[0].SetActive(true);
                break;
            case 0:
                healthIndicatorList[2].SetActive(false);
                healthIndicatorList[1].SetActive(false);
                healthIndicatorList[0].SetActive(false);
                break;
            default:
                healthIndicatorList[2].SetActive(true);
                healthIndicatorList[1].SetActive(true);
                healthIndicatorList[0].SetActive(true);
                break;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        gameOverCG.alpha = 0;
        gameOverCG.gameObject.SetActive(true);

        LeanTween.alphaCanvas(gameOverCG, 1f, 0.25f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() => 
        {
            gameOverButtonsCG.alpha = 0f;
            gameOverButtonsCG.gameObject.SetActive(true);
            LeanTween.alphaCanvas(gameOverButtonsCG, 1f, 0.25f).setEase(LeanTweenType.easeInOutSine).setDelay(0.5f);
        });
    }

    public void NextStageIndicator()
    {
        Time.timeScale = 0;

        stageCompleteCG.alpha = 0;
        stageCompleteCG.gameObject.SetActive(true);

        LeanTween.alphaCanvas(stageCompleteCG, 1f, 0.25f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
        {
            stageCompleteButtonsCG.alpha = 0f;
            stageCompleteButtonsCG.gameObject.SetActive(true);
            LeanTween.alphaCanvas(stageCompleteButtonsCG, 1f, 0.25f).setEase(LeanTweenType.easeInOutSine).setDelay(0.5f);
        });
    }

    #region BUTTONS

    public void ChangeSceneButton(string scene)
    {
        GameManager.Instance.sceneController.CurrentScene = scene;
    }

    public void DialogueEnabler(bool value) => dialogueObj.SetActive(value);

    #endregion
}
