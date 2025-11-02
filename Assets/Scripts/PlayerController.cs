using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public InputAction MoveAction;

    private Rigidbody2D rb;
    private Vector2 move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + speed * move * Time.deltaTime;
        rb.MovePosition(position);
    }
}
