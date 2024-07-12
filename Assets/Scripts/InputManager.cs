using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Camera targetCamera;
    public Paddle leftPaddle;
    public Paddle rightPaddle;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        var mouseScreenPosition = Input.mousePosition;

        mouseScreenPosition.z = targetCamera.WorldToScreenPoint(transform.position).z;
        var clickPosition = targetCamera.ScreenToWorldPoint(mouseScreenPosition);

        if (clickPosition.x > 0f)
        {
            rightPaddle.MoveToY(clickPosition.y);
        }
        else
        {
            leftPaddle.MoveToY(clickPosition.y);
        }
    }
}