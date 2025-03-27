using UnityEngine;

public class DamageHandlerBullet : MonoBehaviour
{
    int bulletHealth = 1;
    public float timer = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        bulletHealth--;
    }
    void Update()
    {
        if(bulletHealth <= 0)
        {
            destroy();
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    void destroy()
    {
        Destroy(gameObject);
    }
}
