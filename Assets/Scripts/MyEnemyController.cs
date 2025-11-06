using Unity.VisualScripting;
using UnityEngine;

public class MyEnemyController : MonoBehaviour
{
    public float speed = 4f;
    public Transform[] movePoints;
    private Vector2 nextPoint;
    private int nextPointIndex;
    private bool IsEnemy = true;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextPointIndex = 0;
        nextPoint = new Vector2(movePoints[nextPointIndex].transform.position.x, movePoints[nextPointIndex].transform.position.y);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

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
            Vector2 position = (Vector2)transform.position + speed * (nextPoint - (Vector2)transform.position).normalized * Time.fixedDeltaTime;
            rb.MovePosition(position);
            
        }
    }

    public void BecomeFriendly()
    {
        IsEnemy = false;
    }
}
