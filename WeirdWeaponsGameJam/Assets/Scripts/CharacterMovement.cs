// CREATED BY: Gabriel Flores

// This script is a controller for the character and will hold all of its movements



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Health variable
    public int health = 100;
    //Public float variables that determines the characters speed, jump force, and fall force
    public float speed;
    public float jumpForce;
    public float fallForce;
    // Variables to control the Pause menu First gameobject is the pauseMenu it self, next check if game is paused;
    public GameObject pauseMenu;
    public static bool gameIsPaused;
    // private Rigidbody
    private Rigidbody rigidBody;

    //Bool to check if the object is on the ground
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the private Rigidbody to the Rigidbody attached the the object
        rigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.None; // Ensures mouse isn't locked
    }

    // Update is called once per frame
    void Update()
    {

        if(health <= 0)
        {
            Debug.Log("Player dead");
        }
        if(Input.GetKeyDown(KeyCode.Escape)) // If Escaped preseed pause game.
        {
            gameIsPaused = !gameIsPaused;

            PauseGame();
        }
        if(!gameIsPaused) // If the game is unpaused the controls will behave normal
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
        else{ // If game is paused no Input will be acted upon
            if (Input.GetKey(KeyCode.D))
            {
                //transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            //If A is pushed then the character will translate backwards
            if (Input.GetKey(KeyCode.A))
            {
                //transform.Translate(-1 * Vector3.forward * Time.deltaTime * speed);
            }

            // A raycast is sent below the object to see if it is touching the ground then that bool is assigned
            isGrounded = Physics.Raycast(gameObject.transform.position, Vector3.down, 1f);

            // If the spacebar is pushed and the pbject is on the ground, the object will jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                //rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }

            // If the object is in the air then it will be pushed down so it falls faster
            if (!isGrounded && rigidBody.velocity.y < 0f)
            {
                //rigidBody.AddForce(Vector3.down * fallForce);
            }
        }

    }

    public void PauseGame()
    {
        pauseMenu.SetActive(gameIsPaused); //display or undisplay pause menu
        if(gameIsPaused)
        {
            Time.timeScale = 0f; // freezes time 
            AudioListener.pause = true; // pause sound
        }
        else {
            Time.timeScale = 1; // sets time back to normal scale
            AudioListener.pause = false; // unpause sound
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Player health = {health}");
 
    }


    
}
