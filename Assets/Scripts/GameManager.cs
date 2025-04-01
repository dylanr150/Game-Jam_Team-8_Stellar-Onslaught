using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private Player Player;
    [SerializeField] private GameObject hintsCanvas;

    public int CurrentLevelIndex = 1; // Start at Level1
    private string[] levels = { "Level1", "Level2", "Level3" };
    private bool showTutorial = false;
    private bool hasWon = false;

    public void DisableTutorial() 
    public int PlayerHealth = 3;
    private bool playerDied = false;

    public float delayBeforeSceneChange = 2f;

    public void SetPlayerHealth(int health)
    {
        playerDied = true;
        PlayerHealth = health;
        LoadSceneWithDelay("MainMenu");
    }

    public int GetPlayerHealth()
    {
        return PlayerHealth;
    }

    public void KillPlayer()
    {
        showTutorial = false;
    }

    public bool ShowTutorial() 
    {
        return showTutorial;
    }

    public void EndGame(bool hasWon) 
    {
        this.hasWon = hasWon;
        DisableTutorial();
        SceneManager.LoadScene("GameEnd");
    }

    public void LoseGame() => EndGame(false);
    public void WinGame() => EndGame(true);
    public bool GetGameOutcome() => hasWon;

    public void PlayerShooting()
    {
        Player.playerShoot();
    }

    public void PlayerStopShooting()
    {
        Player.onStopShooting();
    }

    public void LoadSceneWithDelay(string sceneName)
    {
        StartCoroutine(LoadSceneAfterDelay(sceneName));
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeSceneChange);

        SceneManager.LoadScene(sceneName);
    }
    public void CompleteLevel()
    {
        if (playerDied)
        {
            playerDied = false;
            ResetGame();
            return;
        }

        if (CurrentLevelIndex < levels.Length)
        {
            // Load SkillShop after each level
            LoadSceneWithDelay("SkillShop");

            // Print level completed to console
            Debug.Log("Level " + CurrentLevelIndex + " completed! Loading shop...");
        }
        else
        {
            WinGame();
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
        PlayerHealth = 3; //Sets hp back to 3
        playerDied = false;
        // Reset skill data here
        PlayerSkillManager.Instance.ResetAllSkills();

        // If needed, reset other systems here too
    }
}
