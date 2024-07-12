using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float speed = 50f;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.AddForce(GetInitialDirection() * speed);
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.AddForce(force);
    }

    private static Vector2 GetInitialDirection()
    {
        var x = Random.value > 0.5f ? 1f : -1f;
        var y = Random.value > 0.5f
            ? Random.Range(0.5f, 1.5f)
            : Random.Range(-1.5f, -0.5f);

        return new Vector2(x, y).normalized;
    }
}