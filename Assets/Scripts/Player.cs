using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;    // base movement speed (will be overridden based on MoveSpeed level)
    [SerializeField] private int health = 1;
    public float pauseDuration = 2f;

    public GameObject bulletPrefab;
    private GameObject gunSpot;
    private GameObject gunSpot2;

    private bool dead = false;

    private Rigidbody2D rb;

    public Animator animator;

    void Start()
    {
        // Apply MoveSpeed upgrade
        int moveSpeedLevel = PlayerSkillManager.Instance.GetSkillLevel("MoveSpeed");
        float baseSpeed = 5.0f;             // adjust as desired
        float incrementPerLevel = 1.0f;     // how much speed to add per level
        speed = baseSpeed + (moveSpeedLevel - 1) * incrementPerLevel;

        // Initialize other components
        rb = GetComponent<Rigidbody2D>();
        InputManager.Instance.OnMove.AddListener(MovePlayer);
        InputManager.Instance.OnShoot.AddListener(playerShoot);
        InputManager.Instance.StopShoot.AddListener(onStopShooting);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        health--;
    }

    void Update()
    {
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(PauseGame());
        Destroy(gameObject);
    }

    public void MovePlayer(Vector2 direction)
    {
        if (direction != Vector2.zero)
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
        gunSpot = GameObject.Find("GunSpotL");
        gunSpot2 = GameObject.Find("GunSpotR");
        Instantiate(bulletPrefab, gunSpot.transform.position, Quaternion.identity);
        Instantiate(bulletPrefab, gunSpot2.transform.position, Quaternion.identity);

        animator.SetBool("isShoot", true);
    }

    public void onStopShooting()
    {
        animator.SetBool("isShoot", false);
    }

    IEnumerator PauseGame()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(pauseDuration);
        Time.timeScale = 1;
        Debug.Log("Game resumed!");
    }
}
