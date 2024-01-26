//CREATED BY: Gabriel Flores

// This script will contain the code to allow the player to shoot a projectile in a direction



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public AudioSource source;
    // A private variable that will hold a cloned game object and its particle system
    private GameObject shotBullet;
    private ParticleSystem shotBulletParticle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // When LMB is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // The game object is cloned and stored
            shotBullet = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);

            // The cloned game objects particle system is set so when the particle stops it is destroyed
            shotBulletParticle = shotBullet.GetComponent<ParticleSystem>();
            var main = shotBulletParticle.main;
            main.stopAction = ParticleSystemStopAction.Destroy;

            //The cloned game objects script is destroyed to prevent it from cloning itself further
            Destroy(shotBullet.GetComponent<PlayerShoot>());

            // The particle effect is played
            shotBulletParticle.Play();
            source.PlayOneShot(source.clip);

            
            
        }
    }



}
