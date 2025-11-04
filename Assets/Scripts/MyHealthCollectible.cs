using UnityEngine;

public class MyHealthCollectible : MonoBehaviour
{
    public int amount = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(amount);
            Destroy(gameObject);
        }
    }
}
