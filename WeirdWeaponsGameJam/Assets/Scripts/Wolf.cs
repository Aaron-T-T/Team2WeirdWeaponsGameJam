// CREATED BY: Gabriel Flores

//This script will hold all of the varibales and behaviors for the Wolf enemy


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem wolfBullet;

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
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackMode)
        {
            Patrol();
        }
        else if (attackMode)
        {
            Attack();
        }
        Debug.Log(rand);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            attackMode = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            attackMode = false;
        }
    }

    private void Patrol()
    {
        transform.Translate(patrolDirection * patrolSpeed * Time.deltaTime, Space.Self);

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
        if (!takingAction)
        {
            rand = Random.Range(1, 3);
            takingAction = true;
        }

        if (rand == 1 && Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * chaseSpeed);
        }
        else if (rand == 1 && !takingMelee)
        {
            MeleeAttack();
        }
        
        if (rand == 2 && !takingShot)
        {
            Shoot();
        }

    }

    private void MeleeAttack()
    {
        takingMelee = true;
        StartCoroutine(postAttackDelay());    
    }
    
    private void Shoot()
    {
        takingShot = true;
        StartCoroutine(postShotDelay());
    }

   IEnumerator postAttackDelay()
    {
        //Would play attack animeation

        yield return new WaitForSeconds(2f);
        takingAction = false;
        takingMelee = false;
        Debug.Log("ActionChange");
    }

    IEnumerator postShotDelay()
    {
        clonedBullet = Instantiate(wolfBullet, wolfBullet.transform.position, wolfBullet.transform.rotation);
        clonedBullet.Play();
        yield return new WaitForSeconds(2f);
        takingShot = false;
        takingAction = false;
        Debug.Log("ActionChange");
    }
}
