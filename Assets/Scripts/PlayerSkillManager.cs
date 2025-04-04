using UnityEngine;
using System.Collections.Generic;

public class PlayerSkillManager : MonoBehaviour
{
    public static PlayerSkillManager Instance;

    private Dictionary<string, int> skillLevels = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Auto-create if it's not in the scene
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EnsureInstanceExists()
    {
        if (Instance == null)
        {
            GameObject managerGO = new GameObject("PlayerSkillManager (Auto)");
            Instance = managerGO.AddComponent<PlayerSkillManager>();
            DontDestroyOnLoad(managerGO);
        }
    }

    public int GetSkillLevel(string skillName)
    {
        if (!skillLevels.ContainsKey(skillName))
        {
            skillLevels[skillName] = 1;
        }
        return skillLevels[skillName];
    }

    public void UpgradeSkill(string skillName)
    {
        if (!skillLevels.ContainsKey(skillName))
        {
            skillLevels[skillName] = 1;
        }

        skillLevels[skillName]++;
    }

    // Resets all skills to their default state.
    public void ResetAllSkills()
    {
        skillLevels.Clear();
    }

    // Optionally allows setting a specific skill level manually.
    public void SetSkillLevel(string skillName, int level)
    {
        skillLevels[skillName] = Mathf.Max(1, level); // Optional: clamp to 1+
    }
}