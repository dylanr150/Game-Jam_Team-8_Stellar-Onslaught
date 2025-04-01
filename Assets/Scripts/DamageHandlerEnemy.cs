using Unity.VisualScripting;
using UnityEngine;

public class DamageHandlerEnemy : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int scoreValue = 100;
    [SerializeField] private float deathTime = 0.5f;

    private bool dead = false;

    public Animator animator;
    public Animator engineAnimator;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        health--;
    }
    void Update()
    {
        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;
        animator.SetBool("isDead", true);
        
        if (engineAnimator != null)
        {
            engineAnimator.SetBool("isDead", true);
        }
        // Disable the collider so the enemy can't be hit again
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, deathTime);
        ScoreManager.Instance.AddScore(scoreValue);
        GameSoundController.Instance.PlayShipDie();
        FindFirstObjectByType<LevelController>().EnemyDestroyed();
    }


}
