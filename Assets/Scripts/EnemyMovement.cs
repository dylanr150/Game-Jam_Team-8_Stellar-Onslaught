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

    private int currentWaypoint = 0;
    private float fireTimer = 0;
    private bool inFormation = false;
    private bool isDiving = false;

    void Start()
    {
        Debug.Log($"{gameObject.name} spawned at {transform.position}");
    }

    void Update()
    {
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
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Debug.Log($"{gameObject.name} shooting bullet from {firePoint.position}");
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            fireTimer = 0;
        }
    }

    // Check if the enemy should dive at the player
    void CheckForDive()
    {
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
        while (Vector2.Distance(transform.position, player.position) > 0.2f)
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

    void OnDestroy()
    {
        Debug.LogError($"{gameObject.name} was destroyed at {transform.position}");
    }
}
