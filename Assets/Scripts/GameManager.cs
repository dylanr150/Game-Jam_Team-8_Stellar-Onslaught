using TMPro;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager> // No need to declare Instance here.
{
    [SerializeField] private Player Player;

    public TextMeshProUGUI scoreText;
    public int currentScore = 0;


    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int value)
    {
        currentScore += value;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
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
}
