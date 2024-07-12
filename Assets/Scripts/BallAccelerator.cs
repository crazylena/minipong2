using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BallAccelerator : MonoBehaviour
{
    public float acceleration = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var ball = collision.gameObject.GetComponent<Ball>();

        if (ball == null)
        {
            return;
        }

        var normal = collision.GetContact(0).normal;
        ball.AddForce(-normal * acceleration);
    }
}