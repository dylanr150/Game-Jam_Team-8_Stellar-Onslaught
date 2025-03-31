using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string enemyTag = "Enemy";  // Tag to identify enemies
    private int totalEnemies;         // Total number of enemies in the level
    private int remainingEnemies;     // Number of enemies still alive

    void Start()
    {
        // Count the number of enemies at the start of the level
        totalEnemies = GameObject.FindGameObjectsWithTag(enemyTag).Length;
        remainingEnemies = totalEnemies;
    }

    void Update()
    {
        // Check if all enemies are destroyed
        if (remainingEnemies <= 0)
        {
            CompleteLevel();
        }
    }

    // Call this method when an enemy is destroyed
    public void EnemyDestroyed()
    {
        remainingEnemies--; // Decrease the number of remaining enemies
    }

    // Call this method when the level is completed
    private void CompleteLevel()
    {
        // Load the next scene by index
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if there’s a next scene available
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels to load.");
            // Optionally, you can load a main menu or show a victory screen
        }
    }
}
