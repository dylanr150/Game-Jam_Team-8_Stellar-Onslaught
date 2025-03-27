using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;

    public GameObject bulletPrefab;
    private GameObject gunSpot;


    private Rigidbody2D rb;

    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnShoot.AddListener(playerShoot);
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
        gunSpot = GameObject.Find("GunSpot");
        Instantiate(bulletPrefab, gunSpot.transform.position, Quaternion.identity);
    }

}
