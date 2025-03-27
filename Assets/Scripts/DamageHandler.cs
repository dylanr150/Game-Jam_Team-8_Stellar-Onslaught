using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    int health = 1;

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
    }
}
