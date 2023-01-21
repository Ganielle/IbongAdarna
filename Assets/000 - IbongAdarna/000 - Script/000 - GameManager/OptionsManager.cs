using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private bool gameplay;
    [ConditionalField("gameplay")] [SerializeField] private GameObject settingsObj;

    private void Awake()
    {
        if (gameplay) volumeSlider.value = GameManager.Instance.SoundMnger.CurrentVolume;
    }

    public void ChangeVolume()
    {
        GameManager.Instance.SoundMnger.CurrentVolume = volumeSlider.value;
        PlayerPrefs.SetFloat("volumeData", GameManager.Instance.SoundMnger.CurrentVolume);
    }

    public void CheckVolume()
    {
        volumeSlider.value = GameManager.Instance.SoundMnger.CurrentVolume;
    }

    public void SettingsEnabler(bool value)
    {
        if (!gameplay) return;

        settingsObj.SetActive(value);
    }
}
