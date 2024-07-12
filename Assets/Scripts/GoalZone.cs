using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GoalZone : MonoBehaviour
{
    public Action OnGoalReached;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var ball = other.gameObject.GetComponent<Ball>();

        if (ball == null)
        {
            return;
        }

        OnGoalReached?.Invoke();
    }
}