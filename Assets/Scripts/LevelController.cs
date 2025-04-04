using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] EnemyManger enemyManager;
    public static LevelController Instance;
    public string enemyTag = "Enemy";
    private int totalEnemies;
    private int remainingEnemies;
    private bool levelCompleted = false; // 👈 Prevents multiple calls

    void Start()
    {
        totalEnemies = enemyManager.GetEnemyCount();
        remainingEnemies = totalEnemies;
    }

    void Update()
    {
        if (!levelCompleted && remainingEnemies <= 0)
        {
            CompleteLevel();
        }
    }

    public void EnemyDestroyed()
    {
        remainingEnemies--;

        // Clamp the value to avoid going negative
        remainingEnemies = Mathf.Max(remainingEnemies, 0);
    }

    private void CompleteLevel()
    {
        levelCompleted = true; //Prevent repeated triggering
        GameManager.Instance.CompleteLevel();
    }
}
