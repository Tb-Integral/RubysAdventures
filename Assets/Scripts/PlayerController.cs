using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0.0f;
        float vertical = 0.0f;

        if (Keyboard.current.leftArrowKey.IsPressed() || Keyboard.current.aKey.IsPressed())
        {
            horizontal = -1.0f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.IsPressed())
        {

            horizontal = 1.0f;

        }
        else
        {
            horizontal = 0f;
        }

        if (Keyboard.current.leftArrowKey.IsPressed() || Keyboard.current.sKey.IsPressed())
        {
            vertical = -1.0f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.wKey.IsPressed())
        {

            vertical = 1.0f;

        }
        else
        {
            vertical = 0f;
        }

        Vector2 position = transform.position;
        position.x = position.x + 0.1f * horizontal;
        position.y = position.y + 0.1f * vertical;
        transform.position = position;
    }
}
