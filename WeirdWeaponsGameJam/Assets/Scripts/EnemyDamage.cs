//CREATED BY: Gabriel Flores

// This script will hold an enemys health and include a way for it to decrease
// Once it is 0 the enemy will be destroyed

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{


    public float health;
    public AudioSource source;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }



    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "PlayerBullet")
        {
            health -= 1;
            source.PlayOneShot(source.clip);
        }
    }
}


