using UnityEngine;

public class TurretCat : MonoBehaviour
{
    bool playerFound = false;
    public Transform target; // Reference to the enemy target
    public Transform gunBarrel; // Reference to the gun barrel for particle spawn
    public ParticleSystem bulletParticles; // Reference to the particle system for bullets
    public float rotationSpeed = 5f; // Rotation speed of the turret
    public float fireRate = 1f; // Rate of fire in bullets per second
    public int AmmoCount = 50;
    public AudioSource source;


    private float nextFireTime = 0f; // Time of the next allowed fire

    void Update()
    {
        if(playerFound && AmmoCount > 0)
        {
            unloadBullets();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player spotted");
            playerFound = true;
        }
    }
    void unloadBullets()
    {
        if(Time.time >= nextFireTime)
        {
            
            bulletParticles.Play();
            nextFireTime = Time.time + 1f / fireRate;
            AmmoCount -=1;
        }
        
    }
}
