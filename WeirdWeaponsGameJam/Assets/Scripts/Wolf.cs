// CREATED BY: Gabriel Flores

//This script will hold all of the varibales and behaviors for the Wolf enemy


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem wolfBullet;
    public AudioSource source;

    public float patrolSpeed;
    public float chaseSpeed;
    public float patrolDistance;

    private bool attackMode = false;
    private bool takingAction = false;
    private bool takingShot = false;
    private bool takingMelee = false;
    private int rand;

    private Vector3 startingPosition;
    private Vector3 patrolDirection = Vector3.forward;
    private ParticleSystem clonedBullet;
    
    // Start is called before the first frame update
    void Start()
    {
        // When the program starts the wofs starting position is stored
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see whether or not if the wolf is in attack mode 
        // If not the the Patrol function is run
        if (!attackMode)
        {
            Patrol();
        }
        else if (attackMode)
        {
            Attack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //When the player enters the wolfs range trigger attack mode is set to true
        if (other.tag == "Player")
        {
            attackMode = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //When the player exits the wolfs range trigger attack mode is set to false so it goes back to patrolling
        if (other.tag == "Player")
        {
            attackMode = false;
            rand = 0;
        }
    }

    private void Patrol()
    {
        // The wolf moves forward
        transform.Translate(patrolDirection * patrolSpeed * Time.deltaTime, Space.Self);

        //If it is a certain distance from its starting position in wither direction then it will turn around and move in the opposite direction
        if(transform.position.x > startingPosition.x + patrolDistance)

        {
            transform.Rotate(0f, -90f, 0f);
            patrolDirection = Vector3.forward;
        }
        else if (transform.position.x < startingPosition.x -patrolDistance)
        {
            transform.Rotate(0f, 90f, 0f);
            patrolDirection = Vector3.forward;
        }
    }

    private void Attack()
    {
        // A random number is generated and taking action is set to true
        if (!takingAction)
        {
            rand = Random.Range(1, 3);
            takingAction = true;
        }

        // If the random number is 1 the wolf will preform a meelee attack
        // It will move towards the player until it is a certain distance away 
        if (rand == 1 && Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * chaseSpeed);
        }
        // Once close enough the MeleeAttack function runs
        else if (rand == 1 && !takingMelee)
        {
            MeleeAttack();
        }
        
        // If the random number is 2 the Shoot function will run
        if (rand == 2 && !takingShot)
        {
            Shoot();
        }

    }

    private void MeleeAttack()
    {
        // takingMelee is set to true and a coroutine starts
        takingMelee = true;
        StartCoroutine(postAttackDelay());    
    }
    
    private void Shoot()
    {
        // takingShot is set to true and a coroutine starts
        takingShot = true;
        StartCoroutine(postShotDelay());
    }

   IEnumerator postAttackDelay()
    {
        // Attack animation is played and the program waits for two seconds before setting its bools to false to allow for another action to be taken

        //Would play attack animeation

        //The function waits for two seconds before setting its bools to false to allow for another action to be taken
        yield return new WaitForSeconds(2f);
        takingAction = false;
        takingMelee = false;
    }

    IEnumerator postShotDelay()
    {
        // A cloned particle effect is instantiated and played
        clonedBullet = Instantiate(wolfBullet, wolfBullet.transform.position, wolfBullet.transform.rotation);
        clonedBullet.Play();

        //Audio clip is played
        source.PlayOneShot(source.clip);

        //The function waits before setting its bools to false
        yield return new WaitForSeconds(2f);
        takingShot = false;
        takingAction = false;
    }
}
