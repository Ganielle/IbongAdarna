using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePuzzleMove : MonoBehaviour
{
    [SerializeField] private MovingPlatform rewardObject;
    [SerializeField] private float rewardResetDestination;
    [SerializeField] private float rewardDestination;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PuzzleBox"))
        {
            rewardObject.MoveToDestination(rewardDestination, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PuzzleBox"))
        {
            rewardObject.MoveToDestination(rewardResetDestination, false);
        }
    }
}
