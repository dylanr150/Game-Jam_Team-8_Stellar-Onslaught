using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private Player Player;
    [SerializeField] private GameObject hintsCanvas;

    public int CurrentLevelIndex = 1; // Start at Level1
    private string[] levels = { "Level1", "Level2", "Level3" };
    private bool showTutorial = false;

    public void DisableTutorial() 
    {
        showTutorial = false;
    }

    public bool ShowTutorial() 
    {
        return showTutorial;
    }

    public void LoseGame() 
    {
        // Ensure that tutorial is not shown on every single playthrough.
        DisableTutorial();
        SceneManager.LoadScene("DeathScreen");
    }

    public void PlayerShooting()
    {
        Player.playerShoot();
    }

    public void PlayerStopShooting()
    {
        Player.onStopShooting();
    }

    public void CompleteLevel()
    {
        if (CurrentLevelIndex < levels.Length)
        {
            // Load SkillShop after each level
            SceneManager.LoadScene("SkillShop");

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
