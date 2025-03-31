using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;
    public Vector2 formationPosition;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform player;

    public float moveSpeed = 3f;
    public float fireRate = 2f;
    public float diveSpeed = 4f; // Slowed down dive speed
    public float diveTriggerDistance = 5f;
    public float bulletSpeed = 5f;

    // Timer field to delay enemy actions
    public float startDelay = 5f; // Delay before the enemy starts moving
    private float timer;

    private int currentWaypoint = 0;
    private float fireTimer = 0;
    private bool inFormation = false;
    private bool isDiving = false;

    void Start()
    {
        // Initialize the timer
        timer = startDelay;
        Debug.Log($"{gameObject.name} spawned at {transform.position}. Waiting for {startDelay} seconds.");
    }

    void Update()
    {
        // Countdown the timer
        if (timer > 0)
        {
            timer -= Time.deltaTime; // Reduce the timer by the time passed
            Debug.Log($"{gameObject.name} waiting: {timer:F2} seconds left before starting");
            if(timer > 0)
            {
                GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                GetComponent<Collider2D>().enabled = true;
            }
            return; // Skip other behaviors until timer reaches 0
        }

        // Once the timer reaches 0, start the enemy behaviors
        if (isDiving)
        {
            Debug.Log($"{gameObject.name} is diving, skipping other behaviors.");
            return; // Skip other behaviors if diving
        }

        if (!inFormation)
        {
            MoveToFormation();
        }
        else
        {
            ShootAtIntervals();
            CheckForDive();
        }
    }

    // Move through waypoints and into formation
    void MoveToFormation()
    {
        if (waypoints.Length > 0 && currentWaypoint < waypoints.Length)
        {
            Debug.Log($"{gameObject.name} moving to waypoint {currentWaypoint}: {waypoints[currentWaypoint].position}");
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
            {
                Debug.Log($"{gameObject.name} reached waypoint {currentWaypoint}");
                currentWaypoint++;
            }
        }
        else
        {
            Debug.Log($"{gameObject.name} moving to formation position {formationPosition}");
            transform.position = Vector2.MoveTowards(transform.position, formationPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, formationPosition) < 0.1f)
            {
                Debug.Log($"{gameObject.name} reached formation position");
                inFormation = true;
            }
        }
    }

    // Shooting at regular intervals
    void ShootAtIntervals()
    {
        if (firePoint == null) // Check if firePoint is destroyed or not assigned
        {
            return; // Exit the method if firePoint is missing
        }

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Debug.Log($"{gameObject.name} shooting bullet from {firePoint.position}");

            // Instantiate the bullet at the firePoint's position
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0,0,180));

            // Get the bullet's Rigidbody2D (if it has one) and set the velocity to make it move downwards
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(0, -1) * bulletSpeed;  // Set velocity to move downwards
            }
            else
            {
                Debug.LogWarning($"{gameObject.name} bullet does not have a Rigidbody2D, cannot apply velocity.");
            }

            fireTimer = 0;
        }
    }

    // Check if the enemy should dive at the player
    void CheckForDive()
    {
        if (player == null) // Check if player is destroyed or not assigned
        {
            return; // Exit the method if player is missing
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 0.2% chance per frame for diving
        if (distanceToPlayer < diveTriggerDistance && Random.value < 0.002f)
        {
            Debug.Log($"{gameObject.name} preparing to dive...");
            StartCoroutine(DelayedDive());
        }
    }

    // Add a random delay before diving
    IEnumerator DelayedDive()
    {
        float delay = Random.Range(0.5f, 2f); // Random delay before diving
        yield return new WaitForSeconds(delay);

        Debug.Log($"{gameObject.name} is diving after {delay} seconds!");
        isDiving = true;
        StartCoroutine(DiveAtPlayer());
    }

    // Enemy dive attack
    IEnumerator DiveAtPlayer()
    {
        while (player != null && Vector2.Distance(transform.position, player.position) > 0.2f) // Check if player is still valid
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, diveSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.LogWarning($"{gameObject.name} completed the dive!");

        // Instead of destroying immediately, make the enemy fly downward off-screen
        float exitSpeed = 3f;
        Vector3 exitDirection = new Vector3(0, -1, 0); // Move downward

        while (transform.position.y > -10) // Move until off-screen
        {
            transform.position += exitDirection * exitSpeed * Time.deltaTime;
            yield return null;
        }

        Debug.LogWarning($"{gameObject.name} exited the screen, now destroying.");
        Destroy(gameObject);
    }
}
