using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the projectile collided with an object
        if (collision.gameObject.CompareTag("Player"))
        {
            // If the collided object is the player or an obstacle, apply damage or other logic
            HandleCollision(collision.gameObject);

            // Destroy the projectile after collision
            DestroyProjectile();
        }
    }

    private void HandleCollision(GameObject collidedObject)
    {
        
        if (collidedObject.CompareTag("Player"))
        {
            collidedObject.GetComponent<CharacterMovement>().TakeDamage(damage);
            DestroyProjectile();
        }
        else if (collidedObject.CompareTag("Default"))
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        // Implement any effects 
        // ...

        // Destroy the projectile
        Destroy(gameObject);
    }
}



   

