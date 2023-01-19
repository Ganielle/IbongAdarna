using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFXController : MonoBehaviour
{
    [SerializeField] private AudioClip sfxClip;
    [SerializeField] private bool is3D;
    [ConditionalField("is3D")] [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource.volume = GameManager.Instance.SoundMnger.CurrentVolume;
    }

    public void PlaySFX()
    {
        if (is3D)
            audioSource.PlayOneShot(sfxClip);
        else
            GameManager.Instance.SoundMnger.PlaySFX(sfxClip);
    }
}
