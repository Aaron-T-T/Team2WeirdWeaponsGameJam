//CREATED BY: Gabriel Flores

// This script will contain the code to allow the player to shoot a projectile in a direction



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float bulletSpeed;
    public bool shotOut = false;

    private GameObject shotBullet;
    private ParticleSystem shotBulletParticle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shotBullet = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);

            shotBulletParticle = shotBullet.GetComponent<ParticleSystem>();
            var main = shotBulletParticle.main;
            main.stopAction = ParticleSystemStopAction.Destroy;
            shotBulletParticle.Play();

            Destroy(shotBullet.GetComponent<PlayerShoot>());
            
        }
    }



}
