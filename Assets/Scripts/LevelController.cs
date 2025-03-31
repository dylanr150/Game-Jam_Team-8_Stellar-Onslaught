using UnityEngine;

public class LevelController : MonoBehaviour
{
    public string enemyTag = "Enemy";
    private int totalEnemies;
    private int remainingEnemies;
    private bool levelCompleted = false; // 👈 Prevents multiple calls

    void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag(enemyTag).Length;
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
        levelCompleted = true; // ✅ Prevent repeated triggering
        GameManager.Instance.CompleteLevel();
    }
}