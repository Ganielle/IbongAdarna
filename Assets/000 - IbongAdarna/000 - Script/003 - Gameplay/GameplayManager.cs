using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [field: ReadOnly] [field: SerializeField] public Vector3 RespawnPosition { get; set; }
}
