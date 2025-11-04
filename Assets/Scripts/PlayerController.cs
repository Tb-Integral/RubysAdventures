using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public InputAction MoveAction;
    public int maxHealth = 5;

    private Rigidbody2D rb;
    private Vector2 move;
    private int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
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

    private void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
