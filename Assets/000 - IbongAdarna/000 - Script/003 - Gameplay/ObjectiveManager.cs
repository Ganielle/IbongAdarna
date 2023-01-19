using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    private event EventHandler ArtifactCountChange;
    public event EventHandler OnArtifactCountChange
    {
        add
        {
            if (ArtifactCountChange == null || !ArtifactCountChange.GetInvocationList().Contains(value))
                ArtifactCountChange += value;
        }
        remove { ArtifactCountChange -= value; }
    }
    public int ArtifactCount
    {
        get => artifactCount;
        set
        {
            artifactCount = value;
            ArtifactCountChange?.Invoke(this, EventArgs.Empty);
        }
    }

    //  ================================

    [SerializeField] private TextMeshProUGUI artifactCountTMP;
    [SerializeField] private int totalArtifacts;

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private int artifactCount;

    private void Awake()
    {
        artifactCountTMP.text = "Artifacts Collected " + artifactCount + "/" + totalArtifacts;
        OnArtifactCountChange += ArtifactChange;
    }

    private void OnDisable()
    {
        OnArtifactCountChange -= ArtifactChange;
    }

    private void ArtifactChange(object sender, EventArgs e)
    {
        artifactCountTMP.text = "Artifacts Collected " + artifactCount + "/" + totalArtifacts;
    }
}
