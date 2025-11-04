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

    public int health { get { return currentHealth; }}
    public float timeInvincible = 2f;
    private bool IsInvincible;
    private float damageCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth - 2;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (IsInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                IsInvincible = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + speed * move * Time.deltaTime;
        rb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (IsInvincible)
            {
                return;
            }
            IsInvincible = true;
            damageCooldown = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
