using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public enum MainMenuState
    {
        NONE,
        MAINMENU,
        SELECTSTAGE,
        OPTIONS
    }
    private event EventHandler stateChanged;
    public event EventHandler OnStateChanged
    {
        add
        {
            if (stateChanged == null || !stateChanged.GetInvocationList().Contains(value))
                stateChanged += value;
        }
        remove { stateChanged -= value; }
    }
    public MainMenuState CurrentMenuState
    {
        get => currentMenuState;
        set
        {
            lastMenuState = currentMenuState;
            currentMenuState = value;
            stateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    //  ====================================

    [Header("MAIN MENU")]
    [SerializeField] private CanvasGroup menuCG;
    [SerializeField] private AudioClip bgMusic;
    [SerializeField] private OptionsManager optionsManager;

    [Header("SELECTSTAGE")]
    [SerializeField] private CanvasGroup selectStageCG;
    [SerializeField] private Button previousBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private TextMeshProUGUI stageDescriptionsTMP;
    [SerializeField] private List<GameObject> stages;
    [TextArea][SerializeField] private List<string> descriptionsTxt;
    [SerializeField] private List<string> sceneNames;

    [Header("OPTIONS")]
    [SerializeField] private CanvasGroup optionsCG;

    [Header("DEBUGGER")]
    [ReadOnly][SerializeField] private MainMenuState currentMenuState;
    [ReadOnly][SerializeField] private MainMenuState lastMenuState;
    [ReadOnly][SerializeField] private int stageSelectIndex;

    private void Awake()
    {
        GameManager.Instance.MainCamera.orthographic = true;
        OnStateChanged += MainMenuStateChange;
    }

    private void Start()
    {
        CurrentMenuState = MainMenuState.MAINMENU;
    }

    private void MainMenuStateChange(object sender, EventArgs e)
    {
        AnimationPanels();
    }

    public IEnumerator ChangeBGMusic()
    {
        optionsManager.CheckVolume();
        GameManager.Instance.SoundMnger.SetBGMusic(bgMusic);
        yield return null;
    }

    private void AnimationPanels()
    {
        switch (currentMenuState)
        {
            case MainMenuState.MAINMENU:
                if (lastMenuState == MainMenuState.NONE)
                    GameManager.Instance.Animations.CanvasFade(menuCG, null, null);

                else if (lastMenuState == MainMenuState.SELECTSTAGE)
                    GameManager.Instance.Animations.CanvasFade(menuCG, selectStageCG, null);

                else if (lastMenuState == MainMenuState.OPTIONS)
                    GameManager.Instance.Animations.CanvasFade(menuCG, optionsCG, null);

                break;
            case MainMenuState.SELECTSTAGE:

                if (lastMenuState == MainMenuState.MAINMENU)
                    ResetStageSelectMenu(() =>
                    {
                        GameManager.Instance.Animations.CanvasFade(selectStageCG, menuCG, null);
                    });

                break;
            case MainMenuState.OPTIONS:

                if (lastMenuState == MainMenuState.MAINMENU)
                    GameManager.Instance.Animations.CanvasFade(optionsCG, menuCG, null);

                break;
        }
    }

    #region STAGE SELECT

    public void ResetStageSelectMenu(Action action = null)
    {
        stages[stageSelectIndex].SetActive(false);
        stageSelectIndex = 0;
        stages[0].SetActive(true);
        stageDescriptionsTMP.text = descriptionsTxt[0];
        CheckPreviousNextButton();

        action?.Invoke();
    }

    private void CheckPreviousNextButton()
    {
        if (stageSelectIndex == 0)
        {
            previousBtn.interactable = false;
            nextBtn.interactable = true;
        }

        else if (stageSelectIndex > 0 && stageSelectIndex < stages.Count - 1)
        {
            previousBtn.interactable = true;
            nextBtn.interactable = true;
        }

        else if (stageSelectIndex >= stages.Count - 1)
        {
            previousBtn.interactable = true;
            nextBtn.interactable = false;
        }
    }

    #endregion

    #region BUTTONS

    public void MenuStateButton(int value)
    {
        if (!GameManager.Instance.CanUseButtons)
            return;

        GameManager.Instance.CanUseButtons = false;

        switch (value)
        {
            case (int)MainMenuState.MAINMENU:
                CurrentMenuState = MainMenuState.MAINMENU;
                break;
            case (int)MainMenuState.OPTIONS:
                CurrentMenuState = MainMenuState.OPTIONS;
                break;
            case (int)MainMenuState.SELECTSTAGE:
                CurrentMenuState = MainMenuState.SELECTSTAGE;
                break;
        }
    }

    public void PreviousNextStage(bool isNext)
    {
        if (!GameManager.Instance.CanUseButtons) return;

        GameManager.Instance.CanUseButtons = false;

        if (isNext)
        {
            stageSelectIndex += 1;
            stages[stageSelectIndex - 1].SetActive(false);
            stages[stageSelectIndex].SetActive(true);
        }
        else
        {
            stageSelectIndex -= 1;
            stages[stageSelectIndex + 1].SetActive(false);
            stages[stageSelectIndex].SetActive(true);
        }

        stageDescriptionsTMP.text = descriptionsTxt[stageSelectIndex];
        CheckPreviousNextButton();
        GameManager.Instance.CanUseButtons = true;
    }

    public void PlayStage()
    {
        if (!GameManager.Instance.CanUseButtons) return;

        GameManager.Instance.CanUseButtons = false;

        GameManager.Instance.sceneController.CurrentScene = sceneNames[stageSelectIndex];
    }

    #endregion
}
