using UnityEngine;

public class MyDamageZone : MonoBehaviour
{
    public int amount = -1;
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            if (controller.health > 0)
            {
                controller.ChangeHealth(amount);
                Debug.Log(controller.health + "/" + controller.maxHealth);
            }
            else
            {
                Destroy(controller.gameObject);
            }
        }
    }
}
