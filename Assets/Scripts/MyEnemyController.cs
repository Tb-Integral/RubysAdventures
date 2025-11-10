using Unity.VisualScripting;
using UnityEngine;

public class MyEnemyController : MonoBehaviour
{
    public float speed = 4f;
    public Transform[] movePoints;
    public ParticleSystem smokeEffect;
    public GameObject damageEffect;
    private Vector2 nextPoint;
    private int nextPointIndex;
    private bool IsEnemy = true;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextPointIndex = 0;
        nextPoint = new Vector2(movePoints[nextPointIndex].transform.position.x, movePoints[nextPointIndex].transform.position.y);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (IsEnemy)
        {
            if (Vector2.Distance(transform.position, nextPoint) < 0.1f)
            {
                nextPointIndex++;
                if (nextPointIndex == movePoints.Length)
                {
                    nextPointIndex = 0;
                }
                nextPoint = new Vector2(movePoints[nextPointIndex].transform.position.x, movePoints[nextPointIndex].transform.position.y);
            }
            Vector2 directional = (nextPoint - (Vector2)transform.position).normalized;
            Vector2 position = (Vector2)transform.position + speed * directional * Time.fixedDeltaTime;

            // anim
            animator.SetFloat("Move X", directional.x);
            animator.SetFloat("Move Y", directional.y);

            rb.MovePosition(position);
        }
    }

    public void BecomeFriendly()
    {
        IsEnemy = false;
        rb.simulated = false;
        transform.GetComponent<MyDamageZone>().enabled = false;
        audioSource.Stop();
        smokeEffect.Stop();
        Instantiate(damageEffect, transform.position + Vector3.up, Quaternion.identity);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
