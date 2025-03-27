using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody2D>();
    }

    private void MovePlayer(Vector2 direction)
    {
        rb.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
