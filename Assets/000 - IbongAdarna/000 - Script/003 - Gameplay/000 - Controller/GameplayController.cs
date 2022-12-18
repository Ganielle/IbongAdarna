using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    private GameController gameController;

    private void Awake()
    {
        gameController = new GameController();
    }

    private void OnEnable()
    {
        gameController.Enable();
    }

    private void OnDisable()
    {
        gameController.Disable();
    }

    private void Start()
    {
        
    }
}
