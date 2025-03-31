using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Drag your UI TextMeshPro object here in the Inspector

    private void Start()
    {
        UpdateScoreUI();
    }

    private void Update()
    {
        // Continuously update the score UI if the score changes
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        // Update the UI Text to display the current score
        if (scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.currentScore;
        }
    }
}
