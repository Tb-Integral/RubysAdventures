using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject EnemyFolder;
    private int maxEnemy;
    private int currentCount = 0;

    private void Awake()
    {
        instance = this;
        maxEnemy = EnemyFolder.transform.childCount;
    }

    public void AddEnemy()
    {
        currentCount++;
        if (currentCount == maxEnemy)
        {
            MyUIHandler.instance.TurnOnNPCDialogue(-1);
        }
    }
}
