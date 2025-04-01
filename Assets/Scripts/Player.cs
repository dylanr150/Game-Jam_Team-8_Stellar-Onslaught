using UnityEngine;
using System;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
 //   [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    private int health;

    public GameObject heartPrefab;
    private List<GameObject> hearts = new List<GameObject>();

    [SerializeField] private float fireRate = 1.5f;

    public GameObject bulletPrefab;
    private GameObject gunSpot;
    private GameObject gunSpot2;

    private Boolean dead = false;

    private Rigidbody2D rb;

    public Animator animator;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererEngine;

    private Color originalColor;
    private Color engineColor;

    private float timeSinceLastFire = 0.0f;
    public bool IsFiring { get => timeSinceLastFire < 2.0f; } // Guess a time for the bullet to have passed.

    void Start()
    {
        InputManager.Instance.OnMove.AddListener(MovePlayer);
        InputManager.Instance.OnShoot.AddListener(playerShoot);
        InputManager.Instance.StopShoot.AddListener(onStopShooting);
        rb = GetComponent<Rigidbody2D>();
        health = GameManager.Instance.GetPlayerHealth();

        spriteRenderer = transform.Find("Main Ship - Engines - Base Engine - Powering").GetComponent<SpriteRenderer>();
        spriteRendererEngine = transform.Find("Main Ship - Weapons - Auto Cannon").GetComponent<SpriteRenderer>(); 
        
        originalColor = spriteRenderer.color;
        engineColor = spriteRendererEngine.color;

        SpawnHearts();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        health--;
        if (health > 0) // Only flash if still alive
        {
            StartCoroutine(FlashRed(spriteRenderer, originalColor));
            StartCoroutine(FlashRed(spriteRendererEngine, engineColor));
        }
        GameManager.Instance.SetPlayerHealth(health);
        UpdateHearts();
    }
    void Update()
    {
        if (health <= 0 && !dead)
        {
            Die();
        }

        timeSinceLastFire += Time.deltaTime;
    }

    public void Die()
    {
        dead = true;
        // Disable the collider so the enemy can't be hit again
        GetComponent<Collider2D>().enabled = false;

        // StartCoroutine(PauseGame());
        Destroy(gameObject);

        GameManager.Instance.LoseGame();
    }

    public void MovePlayer(Vector2 direction)
    {
        if(direction != Vector2.zero) 
        {
            rb.linearVelocity = new Vector2(direction.x * speed, 0);
        }

        else
        {
            rb.linearVelocity = Vector2.zero;
        }
        
    }

    public void playerShoot()
    {
        if (timeSinceLastFire > fireRate) 
        {
            timeSinceLastFire = 0.0f;
            gunSpot = GameObject.Find("GunSpotL");
            gunSpot2 = GameObject.Find("GunSpotR");
            Instantiate(bulletPrefab, gunSpot.transform.position, Quaternion.identity);
            Instantiate(bulletPrefab, gunSpot2.transform.position, Quaternion.identity);

            animator.SetBool("isShoot", true);
            GameSoundController.Instance.PlayShipShoot();
        }
    }

    public void onStopShooting()
    {
        animator.SetBool("isShoot", false);
    }

    private void SpawnHearts()
    {
        foreach(GameObject heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();

        for (int i =0; i < health; i++)
        {
            Vector3 heartPosition = new Vector3(-8.5f + (i * 0.4f), -4.6f, 0);
            GameObject heart = Instantiate(heartPrefab, heartPosition, Quaternion.identity);
            hearts.Add(heart);
        }
    }

    private void UpdateHearts()
    {
        // Remove the last heart when health decreases
        if (hearts.Count > health)
        {
            Destroy(hearts[hearts.Count - 1]);
            hearts.RemoveAt(hearts.Count - 1);
        }
    }

    private IEnumerator FlashRed(SpriteRenderer sp, Color originalC)
    {
        sp.color = new Color(1f, 0.3f, 0.3f, 0.5f); // Red with transparency
        yield return new WaitForSeconds(0.1f);
        sp.color = originalC;
    }

}
