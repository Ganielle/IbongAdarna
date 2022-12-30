using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IbongAdarna", menuName = "IbongAdarna/Player/MovementData")]
public class PlayerMovementData : ScriptableObject
{
    [field: SerializeField] public float MovementSpeed { get; private set; }
}
