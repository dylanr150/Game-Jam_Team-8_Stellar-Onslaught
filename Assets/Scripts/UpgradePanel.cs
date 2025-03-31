using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [Header("Upgrade Info")]
    public string skillName;              // Set this in the Inspector
    public TextMeshProUGUI levelText;                // Drag the Text component here
    public Button upgradeButton;          // Drag the Button component here

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
        upgradeButton?.onClick.AddListener(UpgradeSkill);

        // Set initial text
        UpdateLevelText();
    }

    private void UpgradeSkill()
    {
        if (PlayerSkillManager.Instance == null)
        {
            Debug.LogError("PlayerSkillManager.Instance is null during upgrade.");
            return;
        }

        if (ScoreManager.Instance.GetCurrentScore() == 700)
        {
            PlayerSkillManager.Instance.UpgradeSkill(skillName);
            ScoreManager.Instance.SetScore(ScoreManager.Instance.GetCurrentScore() - 700); 
            UpdateLevelText();
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