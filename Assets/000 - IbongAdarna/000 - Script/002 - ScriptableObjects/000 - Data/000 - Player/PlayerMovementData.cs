using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IbongAdarna", menuName = "IbongAdarna/Player/MovementData")]
public class PlayerMovementData : ScriptableObject
{
    [field: SerializeField] public float GroundMovementSpeed { get; private set; }
    [field: SerializeField] public float AirMovementSpeed { get; private set; }
    [field: SerializeField] public float JumpStrength { get; private set; }
    [field: SerializeField] public float JumpHeightMultiplier { get; private set; }

    [field: Header("GROUND ENVIRONMENT")]
    [field: SerializeField] public float GroundRadius { get; private set; }
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }

    [field: Header("WALL ENVIRONMENT")]
    [field: SerializeField] public Vector2 WallFrontCheckPos { get; private set; }
    [field: SerializeField] public Vector2 WallFrontRadius { get; private set; }
    [field: SerializeField] public LayerMask WallLayer { get; private set; }
}
