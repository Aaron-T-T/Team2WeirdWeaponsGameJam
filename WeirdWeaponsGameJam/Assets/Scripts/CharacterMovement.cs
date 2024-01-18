// CREATED BY: Gabriel Flores

// This script is a controller for the character and will hold all of its movements



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //Public float variables that determines the characters speed, jump force, and fall force
    public float speed;
    public float jumpForce;
    public float fallForce;

    // private Rigidbody
    private Rigidbody rigidBody;

    //Bool to check if the object is on the ground
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the private Rigidbody to the Rigidbody attached the the object
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // If D is pushed then the character will translate forward
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        //If A is pushed then the character will translate backwards
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1 * Vector3.forward * Time.deltaTime * speed);
        }

        // A raycast is sent below the object to see if it is touching the ground then that bool is assigned
        isGrounded = Physics.Raycast(gameObject.transform.position, Vector3.down, 1f);

        // If the spacebar is pushed and the pbject is on the ground, the object will jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        // If the object is in the air then it will be pushed down so it falls faster
        if (!isGrounded && rigidBody.velocity.y < 0f)
        {
            rigidBody.AddForce(Vector3.down * fallForce);
        }

    }

    
}
