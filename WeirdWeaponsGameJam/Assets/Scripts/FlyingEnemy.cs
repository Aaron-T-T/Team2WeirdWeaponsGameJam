using UnityEngine;
using System.Collections;


public class FlyingEnemy : MonoBehaviour
{
    public float patrolSpeed = 5f;
    public float chaseSpeed = 10f;
    public float minChaseDistance = 2f; // Minimum distance to maintain during chase
    public float stopChaseDistance = 1f; // Distance to stop chasing the player
    public float spottingDelay = 1.5f; // Delay before the enemy starts chasing
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // Transform representing the point where bullets will be spawned
    public float bulletSpeed = 10f; // Speed of the bullets
    private bool canShoot = true;
    private Transform player;
    private bool isChasing = false;
    private Vector3 patrolDirection = Vector3.right;
    private SphereCollider detectionCollider; // Sphere collider detects player
    private GameObject shotBullet;
    private ParticleSystem shotBulletParticle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        detectionCollider = GetComponent<SphereCollider>();

        // Start the spotting coroutine
        StartCoroutine(SpottingCoroutine());
    }

    IEnumerator SpottingCoroutine()
    {
        yield return new WaitForSeconds(spottingDelay);

        // The delay has passed, now the enemy can start chasing
        isChasing = true;
    }

    void Update()
    {
        if (!isChasing)
        {
            Patrol();
        }
        else
        {
            ChaseAndShoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Stop the spotting coroutine if the player is spotted
            StopCoroutine(SpottingCoroutine());

            isChasing = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            isChasing = false;
        }
    }

    void Patrol()
    {
        // Implement patrolling behavior (move back and forth)
        transform.Translate(patrolDirection * patrolSpeed * Time.deltaTime);

        // Check if patrol boundaries are reached
        if (transform.position.x > 5f)
        {
            // Reached right boundary, switch direction
            patrolDirection = Vector3.left;
        }
        else if (transform.position.x < -5f)
        {
            // Reached left boundary, switch direction
            patrolDirection = Vector3.right;
        }
    }

    void ChaseAndShoot()
    {
        // Calculate direction to the player
        Vector3 directionToPlayer = (new Vector3(player.position.x, player.position.y, 0) - new Vector3(transform.position.x, transform.position.y, 0)).normalized;

        // Check if the distance is less than the minimum chase distance
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopChaseDistance)
        {
            // Move towards the player
            transform.Translate(directionToPlayer * chaseSpeed * Time.deltaTime);
            
            //ShootBullet(directionToPlayer);
        }
        if (canShoot)
        {
                StartCoroutine(ShootWithDelay(directionToPlayer));
        }

        // Implement shooting behavior
        
       
    }
    IEnumerator ShootWithDelay(Vector3 direction)
    {
        // Set canShoot to false to prevent shooting during the delay
        canShoot = false;

        // Shoot the bullet
        ShootBullet(direction);

        // Wait for the specified delay
        yield return new WaitForSeconds(1.0f); // Adjust the delay as needed

        // Set canShoot back to true after the delay
        canShoot = true;
    }
    void ShootBullet(Vector3 direction)
    {
        // Instantiate a bullet at the firePoint position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Add force to the bullet in the calculated direction towards the player
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = direction * bulletSpeed;

        // Destroy the bullet after a certain time (adjust as needed)
        Destroy(bullet, 1.0f);
    }

}
