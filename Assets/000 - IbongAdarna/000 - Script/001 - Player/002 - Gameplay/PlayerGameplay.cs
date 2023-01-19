using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerGameplay : MonoBehaviour
{
    private event EventHandler HealthChange;
    public event EventHandler OnHealthChange
    {
        add
        {
            if (HealthChange == null || !HealthChange.GetInvocationList().Contains(value)) HealthChange += value;
        }
        remove { HealthChange -= value; }
    }
    public int Health
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            HealthChange?.Invoke(this, EventArgs.Empty);
        }
    }

    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private List<GameObject> playerObjs;

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private int currentHealth;

    private void Awake()
    {
        currentHealth = 3;

        OnHealthChange += ChangeHealth;
    }

    private void OnDisable()
    {
        OnHealthChange -= ChangeHealth;
    }

    private void ChangeHealth(object sender, EventArgs e)
    {
        if (Health <= 0)
        {
            GameManager.Instance.SoundMnger.PlaySFX(deathClip);
            gameplayManager.GameOver();
            foreach (GameObject go in playerObjs) go.SetActive(false);
        }
    }

    public void HealthChanger(bool isReduce)
    {
        if (isReduce) Health--;
        else Health++;
    }
}
