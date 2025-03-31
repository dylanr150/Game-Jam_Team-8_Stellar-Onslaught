using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    private int currentScore = 0;

    private void Awake()
    {
        // Singleton initialization.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Uncomment out if you don't want to destroy it even after crossing a scene.
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
}
