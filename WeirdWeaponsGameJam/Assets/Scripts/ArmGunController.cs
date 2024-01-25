using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Created by Aaron Torres
// Allows the mouse to move and rotate the gun
public class ArmGunController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public Transform player; // Reference to the player GameObject with the sphere collider

    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;
        float mouseY = Input.GetAxis("Mouse Y");

        // Convert the mouse position to a point in the game world
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z - Camera.main.transform.position.z));

        // Set the Z-axis of the target position to the current Z position of the gun
        targetPosition.z = transform.position.z;

        // Constrain the target position within the sphere around the player
        targetPosition = ConstrainToSphere(targetPosition);

        // Move the gun to the constrained target position in the X and Y axes
        MoveToTarget(targetPosition);
        RotateGun(targetPosition);
        
       
    }

    Vector3 ConstrainToSphere(Vector3 targetPosition)
    {
        // Calculate the direction from the player to the target position
        Vector3 direction = targetPosition - player.position;

        // Use a raycast to check for collisions excluding the gun
        RaycastHit hit;
        if (Physics.Raycast(player.position, direction.normalized, out hit, player.GetComponent<SphereCollider>().radius))
        {
            if (hit.collider.gameObject != gameObject) // Exclude the gun from collision check
            {
                // Adjust the target position to be just outside the sphere
                targetPosition = player.position + direction.normalized * (hit.distance + 0.1f);
            }
        }

        return targetPosition;
    }

    void MoveToTarget(Vector3 targetPosition)
    {
        // Smoothly move the gun towards the target position in the X and Y axes
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    void RotateGun(Vector3 targetPosition)
    {
        // Calculate the direction to the target
        Vector3 lookDirection = targetPosition - transform.position;

        // Ignore the -axis rotation
        lookDirection.z = 0f;
        //lookDirection.y = 0f;

        if (lookDirection.magnitude > 0.001f)
        {
            // Smoothly rotate the gun to face the mouse cursor
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }   
}
