using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Constrain the target position within the sphere using the sphere collider's radius
        targetPosition = player.position + direction.normalized * Mathf.Min(direction.magnitude, player.GetComponent<SphereCollider>().radius);

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

        // Smoothly rotate the gun to face the mouse cursor
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
