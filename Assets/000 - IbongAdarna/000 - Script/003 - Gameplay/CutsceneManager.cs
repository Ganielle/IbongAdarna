using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private float narrationTime;
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private Scrollbar dialogueSlider; 
    [SerializeField] private AudioClip narration;
    [SerializeField] private bool playonstart;

    //  =======================

    int cutsceneAnimation;
    Coroutine narrationCoroutine;

    //  =======================

    public IEnumerator PlayCutscene()
    {
        if (!playonstart)
        {
            GameManager.Instance.sceneController.ResumeTime = true;
            yield break;
        }
        GameManager.Instance.sceneController.ResumeTime = false;
        dialogueSlider.value = 1f;

        cg.alpha = 0f;
        cg.gameObject.SetActive(true);
        LeanTween.alphaCanvas(cg, 1f, 2f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() => 
        {
            narrationCoroutine = StartCoroutine(PlayNarration());
        });

        yield return null;
    }

    private IEnumerator PlayNarration()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        GameManager.Instance.SoundMnger.PlayVoiceNarration(narration);

        yield return new WaitForSecondsRealtime(10f);
        cutsceneAnimation = LeanTween.value(gameObject, 1f, 0f, narrationTime).setOnUpdate((float val) =>
        {
            dialogueSlider.value = val;
            GameManager.Instance.sceneController.ResumeTime = true;
        }).id;
    }

    public void Skip()
    {
        if (!GameManager.Instance.CanUseButtons) return;

        GameManager.Instance.CanUseButtons = false;

        Time.timeScale = 1f;

        if (cutsceneAnimation != 0) LeanTween.cancel(cutsceneAnimation);

        if (narrationCoroutine != null) StopCoroutine(narrationCoroutine);

        GameManager.Instance.sceneController.ResumeTime = true;

        GameManager.Instance.SoundMnger.StopVoiceNarration();
        LeanTween.alphaCanvas(cg, 0f, 1f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            cg.gameObject.SetActive(false);
            GameManager.Instance.CanUseButtons = true;
        });
    }
}
