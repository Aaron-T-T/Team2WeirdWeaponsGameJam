using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCat : MonoBehaviour
{
    bool playerFound = false;
    public Transform target; // Reference to the enemy target
    public Transform gunBarrel; // Reference to the gun barrel for particle spawn
    public ParticleSystem bulletParticles; // Reference to the particle system for bullets
    public float rotationSpeed = 5f; // Rotation speed of the turret
    public float fireRate = 1f; // Rate of fire in bullets per second
    public AudioSource source;


    private float nextFireTime = 0f; // Time of the next allowed fire

    private ParticleSystem clonedBullet;
    public float reloadTime;
    public int ammoCapacity;
    private int currentAmmo;


    private void Start()
    {
        currentAmmo = ammoCapacity;
    }

    void Update()
    {
        if(playerFound && currentAmmo > 0)
        {
            unloadBullets();
        }

        if(currentAmmo == 0)
        {
            StartCoroutine(Reload());
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left");
            playerFound = false;
        }
    }
    void unloadBullets()
    {
        if(Time.time >= nextFireTime)
        {
            clonedBullet = Instantiate(bulletParticles, bulletParticles.transform.position, bulletParticles.transform.rotation);
            source.PlayOneShot(source.clip);
            clonedBullet.Play();
            nextFireTime = Time.time + 1f / fireRate;
            currentAmmo -=1;
        }
        
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammoCapacity;
    }
    
}
