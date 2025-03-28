using UnityEngine;

public class DamageHandlerEnemy : MonoBehaviour
{
    int health = 1;
    [SerializeField] private int scoreValue = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        health--;
    }
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.AddScore(scoreValue);
    }
}
