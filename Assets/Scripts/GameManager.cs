using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private Player Player;

    public int CurrentLevelIndex = 1; // Start at Level1
    private string[] levels = { "Level1", "Level2", "Level3" };

    public int PlayerHealth = 3;

    public float delayBeforeSceneChange = 2f;

    public void SetPlayerHealth(int health)
    {
        PlayerHealth = health;
    }

    public int GetPlayerHealth()
    {
        return PlayerHealth;
    }

    public void KillPlayer()
    {
        Player.Die();
    }

    public void PlayerShooting()
    {
        Player.playerShoot();
    }

    public void PlayerStopShooting()
    {
        Player.onStopShooting();
    }

    public void LoadSceneWithDelay()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSceneChange);

        SceneManager.LoadScene("SkillShop");
    }
    public void CompleteLevel()
    {

        if (CurrentLevelIndex < levels.Length)
        {
            // Load SkillShop after each level
            LoadSceneWithDelay();

            // Print level completed to console
            Debug.Log("Level " + CurrentLevelIndex + " completed! Loading shop...");
        }
        else
        {
            Debug.Log("All levels completed! Returning to main menu.");
            SceneManager.LoadScene("MainMenu");
            ResetGame();
        }
    }

    public void OnExitShop()
    {
        if (CurrentLevelIndex < levels.Length)
        {
            SceneManager.LoadScene(levels[CurrentLevelIndex]);
            CurrentLevelIndex++;
        }
        else
        {
            Debug.Log("All levels completed!");
            // Load credits or main menu
        }
    }

    public void ResetGame()
    {
        Debug.Log("Resetting game state...");
        CurrentLevelIndex = 1;
        ScoreManager.Instance.SetScore(0);

        // Reset skill data here
        PlayerSkillManager.Instance.ResetAllSkills();

        // If needed, reset other systems here too
    }
}