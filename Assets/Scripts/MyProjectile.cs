using UnityEngine;

public class MyProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float timer = 1f;
    public AudioClip GetEnemySound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 directional, float force)
    {
        rb.AddForce(directional * force);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MyEnemyController enemy = collision.transform.GetComponent<MyEnemyController>();

        if (enemy != null)
        {
            Animator enemyAnimator = enemy.GetComponent<Animator>();
            enemyAnimator.SetTrigger("Fixed");
            enemy.BecomeFriendly();
            enemy.PlaySound(GetEnemySound);
        }

        Destroy(gameObject);
    }
}
