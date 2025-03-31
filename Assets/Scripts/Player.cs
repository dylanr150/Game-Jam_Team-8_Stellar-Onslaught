using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;

    public GameObject bulletPrefab;
    private GameObject gunSpot;
    private GameObject gunSpot2;

    private Rigidbody2D rb;

    public Animator animator;

    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnShoot.AddListener(playerShoot);
        inputManager.StopShoot.AddListener(onStopShooting);
        rb = GetComponent<Rigidbody2D>();
    }

    private void MovePlayer(Vector2 direction)
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

    private void playerShoot()
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

}
