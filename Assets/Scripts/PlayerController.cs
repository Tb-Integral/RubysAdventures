using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.WSA;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public InputAction MoveAction;
    public InputAction TalkAction;
    public int maxHealth = 5;
    public GameObject projectile;
    public AudioClip ProjectileSound;
    public float projectileForce = 300f;
    public AudioClip damageAudio;

    private Rigidbody2D rb;
    private Vector2 move;
    private int currentHealth;

    public int health { get { return currentHealth; } }
    public float timeInvincible = 2f;
    private bool IsInvincible;
    private float damageCooldown;
    private Animator animator;
    private Vector2 moveDirection = new Vector2(0, 1);
    private AudioSource audiosource;
    private bool IsMoving;
    private AudioSource footsteps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        TalkAction.Enable();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth - 2;
        MyUIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        footsteps = transform.GetChild(0).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        IsMoving = !Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f);
        if (IsMoving)
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();

            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else
        {
            if (footsteps.isPlaying)
            {
                footsteps.Pause();
            }
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
            PlayAudio(ProjectileSound);
            Launch();
        }

        if (TalkAction.triggered)
        {
            FindFriend();
        }
    }

    private void FindFriend()
    {
        RaycastHit2D hit = Physics2D.CircleCast(rb.position, 1f, moveDirection, 1f, LayerMask.GetMask("NPC"));

        if (hit.collider != null)
        {
            MyNonPlayerCharacter npc = hit.collider.GetComponent<MyNonPlayerCharacter>();

            if (npc != null)
            {
                if (npc.CompareTag("commonNPC"))
                {
                    MyUIHandler.instance.TurnOnNPCDialogue(0);
                }
                else
                {
                    MyUIHandler.instance.TurnOnNPCDialogue(1);
                }
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
            PlayAudio(damageAudio);
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

    public void PlayAudio(AudioClip clip)
    {
        audiosource.PlayOneShot(clip);
    }
}
