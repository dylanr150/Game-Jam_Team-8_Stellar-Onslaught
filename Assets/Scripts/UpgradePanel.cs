using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [Header("Upgrade Info")]
    public string skillName;                  // Set this in the Inspector
    public TextMeshProUGUI levelText;         // Drag the "Level: X" text component here
    public Button upgradeButton;              // Drag the Upgrade button component here

    [Header("Cost Info")]
    [SerializeField] private TextMeshProUGUI costInfoText; // An extra TextMeshProUGUI for "700 points required" message

    private void Start()
    {
        // Null checks
        if (levelText == null)
            Debug.LogError("Level Text is not assigned in the Inspector!", this);

        if (upgradeButton == null)
            Debug.LogError("Upgrade Button is not assigned in the Inspector!", this);

        if (PlayerSkillManager.Instance == null)
            Debug.LogError("PlayerSkillManager is not in the scene! Make sure it's loaded before this runs.", this);

        // Set up the button listener
        upgradeButton.onClick.AddListener(UpgradeSkill);

        // Set initial text
        UpdateLevelText();

        // Optional: Set the initial costInfoText
        if (costInfoText != null)
        {
            costInfoText.text = "700 points required to upgrade.";
        }
    }

    private void Update()
    {
        // Enable or disable the button based on current score
        if (upgradeButton != null)
        {
            int currentScore = ScoreManager.Instance.GetCurrentScore();

            if (currentScore < 700)
            {
                upgradeButton.interactable = false;

                if (costInfoText != null)
                {
                    costInfoText.text = "Not enough points! (700 required)";
                }
            }
            else
            {
                upgradeButton.interactable = true;

                if (costInfoText != null)
                {
                    costInfoText.text = "Press the button to upgrade (" + currentScore + " pts available)";
                }
            }
        }
    }

    private void UpgradeSkill()
    {
        if (PlayerSkillManager.Instance == null)
        {
            Debug.LogError("PlayerSkillManager.Instance is null during upgrade.");
            return;
        }

        // Check again before upgrading, in case score changed
        int currentScore = ScoreManager.Instance.GetCurrentScore();
        if (currentScore >= 700)
        {
            PlayerSkillManager.Instance.UpgradeSkill(skillName);
            ScoreManager.Instance.SetScore(currentScore - 700);
            UpdateLevelText();
        }
        else
        {
            Debug.Log("Not enough score to upgrade!");
        }
    }

    private void UpdateLevelText()
    {
        if (PlayerSkillManager.Instance == null)
            return;

        int level = PlayerSkillManager.Instance.GetSkillLevel(skillName);
        levelText.text = "Level " + level;
    }
}
