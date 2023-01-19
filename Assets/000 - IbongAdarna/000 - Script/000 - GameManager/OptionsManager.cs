using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    public void ChangeVolume()
    {
        GameManager.Instance.SoundMnger.CurrentVolume = volumeSlider.value;
    }

    public void CheckVolume()
    {
        volumeSlider.value = GameManager.Instance.SoundMnger.CurrentVolume;
    }
}
