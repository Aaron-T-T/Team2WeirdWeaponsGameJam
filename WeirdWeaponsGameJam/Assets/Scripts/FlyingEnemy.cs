using UnityEngine;
using System.Collections;


public class FlyingEnemy : MonoBehaviour
{
    public AudioSource source;
    public float patrolSpeed = 5f;
    public float chaseSpeed = 10f;
    public float patrolDistance;
    public float stopChaseDistance = 1f; // Distance to stop chasing the player
    public float spottingDelay = 1.5f; // Delay before the enemy starts chasing
    public ParticleSystem bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // Transform representing the point where bullets will be spawned
    public float bulletSpeed = 10f; // Speed of the bullets
    private bool canShoot = true;
    private Transform player;
    private bool isChasing = false;
    private Vector3 startingPosition;
    private Vector3 patrolDirection = Vector3.forward;
    private SphereCollider detectionCollider; // Sphere collider detects player
    private GameObject shotBullet;
    private ParticleSystem shotBulletParticle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        detectionCollider = GetComponent<SphereCollider>();
        startingPosition = transform.position;

        // Start the spotting coroutine
        StartCoroutine(SpottingCoroutine());
    }

    IEnumerator SpottingCoroutine()
    {
        yield return new WaitForSeconds(spottingDelay);

        
    }

    void Update()
    {
        Debug.Log("Position: " + transform.position);
        
        //Debug.Log(isChasing);
        if (!isChasing)
        {
            Patrol();
        }
        if(isChasing)
        {
            ChaseAndShoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Racoon detected something: " + other.name);
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
        //Debug.Log("Racoon lost sight of something: " + other.name);
        // Check if the exiting collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            isChasing = false;
        }
    }

    void Patrol()
    {
        //Debug.Log("Racoon is patroling");
        transform.Translate(patrolDirection * patrolSpeed * Time.deltaTime, Space.Self);

        //If it is a certain distance from its starting position in wither direction then it will turn around and move in the opposite direction
        if(transform.localPosition.x > startingPosition.x + patrolDistance)

        {
            transform.Rotate(0f, -180f, 0f);
            
            
        }
        else if (transform.localPosition.x < startingPosition.x -patrolDistance)
        {
            transform.Rotate(0f, 180f, 0f);
           
        }
    }

    void ChaseAndShoot()
    {
        
        //Debug.Log("Raccoon is chasing and shooting");
        // Calculate direction to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        Debug.Log("Direction: " + directionToPlayer);
        float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

        if (dotProduct < 0)
        {
            // Player is behind, turn around
            transform.Rotate(0f, 180f, 0f);
        }

        // Check if the distance is less than the minimum chase distance
        //float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // if (distanceToPlayer > stopChaseDistance)
        // {
        //     // Move towards the player
        //     //transform.Translate(directionToPlayer * chaseSpeed * Time.deltaTime);
            
        //     //ShootBullet(directionToPlayer);
        // }
        if (canShoot)
        {
                StartCoroutine(ShootWithDelay(directionToPlayer));
        }
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
        shotBulletParticle = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Get the ParticleSystem component from the instantiated particle system
        var main = shotBulletParticle.main;
        main.stopAction = ParticleSystemStopAction.Destroy;
        shotBulletParticle.Play();
        // The particle effect is played
        

        // The Rigidbody component is used to apply force to the particle system in the calculated direction towards the player
        // var particleRb = shotBulletParticle.GetComponent<Rigidbody>();
        // particleRb.velocity = direction * bulletSpeed;

       

        // Play the audio source for shooting
        source.PlayOneShot(source.clip);

       
    }

}
