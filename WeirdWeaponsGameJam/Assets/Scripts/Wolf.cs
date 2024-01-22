// CREATED BY: Gabriel Flores

//This script will hold all of the varibales and behaviors for the Wolf enemy


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public float patrolSpeed;
    public float patrolDistance;

    private Vector3 startingPosition;
    private bool attackMode = false;
    private bool takingAction = false;

    private CapsuleCollider detectionSpace;
    private Vector3 patrolDirection = Vector3.forward;
    
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
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            attackMode = true;
        }
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
            Debug.Log("Moving left");
            patrolDirection = Vector3.back;
        }
        else if (transform.position.x < startingPosition.x -patrolDistance)
        {
            Debug.Log("Moving right");
            patrolDirection = Vector3.forward;
        }
    }
}
