using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public InputAction MoveAction;
    public int maxHealth = 5;
    public GameObject projectile;
    public float projectileForce = 300f;

    private Rigidbody2D rb;
    private Vector2 move;
    private int currentHealth;

    public int health { get { return currentHealth; }}
    public float timeInvincible = 2f;
    private bool IsInvincible;
    private float damageCooldown;
    private Animator animator;
    private Vector2 moveDirection = new Vector2(0, 1);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth - 2;
        MyUIHandler.instance.SetHealthValue(currentHealth/(float)maxHealth);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        // anim
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (IsInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                IsInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
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
            animator.SetTrigger("Hit");

            IsInvincible = true;
            damageCooldown = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        MyUIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    private void Launch()
    {
        GameObject proj = Instantiate(projectile, rb.position + Vector2.up, Quaternion.identity);

        proj.GetComponent<MyProjectile>().Launch(moveDirection, projectileForce);

        animator.SetTrigger("Launch");
    }
}
