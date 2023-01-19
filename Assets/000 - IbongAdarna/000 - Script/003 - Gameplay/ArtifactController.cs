using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private Image dialogueImg;
    [SerializeField] private Sprite dialogueSprite;

    public void DialogueEnabler()
    {
        dialogueImg.sprite = dialogueSprite;
        dialogueUI.SetActive(true);
    }
}
