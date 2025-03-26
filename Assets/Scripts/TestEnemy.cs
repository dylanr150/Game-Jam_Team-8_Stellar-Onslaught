using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] private int scoreValue = 100;

    void Update()
    {
        // For testing: destroyed when ‘K’ is pressed on the keyboard
        if (Input.GetKeyDown(KeyCode.K))
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Debug.Log("[TestEnemy] DestroyEnemy called. scoreValue = " + scoreValue);
        
        // Score addition (assuming ScoreManager is on the scene).
        ScoreManager.Instance.AddScore(scoreValue);

        // Destroy the enemy (this object).
        Destroy(gameObject);
    }
}
