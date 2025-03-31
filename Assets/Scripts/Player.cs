using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
 //   [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private int health = 1;
    public float pauseDuration = 2f;

    public GameObject bulletPrefab;
    private GameObject gunSpot;
    private GameObject gunSpot2;

    private Boolean dead = false;

    private Rigidbody2D rb;

    public Animator animator;

    void Start()
    {
        InputManager.Instance.OnMove.AddListener(MovePlayer);
        InputManager.Instance.OnShoot.AddListener(playerShoot);
        InputManager.Instance.StopShoot.AddListener(onStopShooting);
        rb = GetComponent<Rigidbody2D>();
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
        // Disable the collider so the enemy can't be hit again
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(PauseGame());
        Destroy(gameObject);
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
        // Pause the game by setting time scale to 0
        Time.timeScale = 0;

        //show a message or play an animation here while the game is paused

        //Wait for the specified duration
        yield return new WaitForSecondsRealtime(pauseDuration); // Use WaitForSecondsRealtime to ignore timeScale

        // Resume the game by setting time scale back to 1
        Time.timeScale = 1;
        Debug.Log("Game resumed!");

        // Optionally, restart or load a scene, or perform any other necessary actions here
    }

}
