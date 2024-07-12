using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    public float speed = 50f;

    private Rigidbody2D _rigidbody;
    private Coroutine _moveCoroutine;

    public void MoveToY(float y)
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }

        _moveCoroutine = StartCoroutine(DoMoveToY(y));
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private IEnumerator DoMoveToY(float y)
    {
        var targetPosition = new Vector2(
            _rigidbody.position.x,
            y
        );

        while (Vector3.Distance(_rigidbody.position, targetPosition) > 0.01f)
        {
            _rigidbody.position = Vector3.MoveTowards(
                _rigidbody.position,
                targetPosition,
                speed * Time.deltaTime
            );

            yield return null;
        }

        _rigidbody.position = targetPosition;
    }
}