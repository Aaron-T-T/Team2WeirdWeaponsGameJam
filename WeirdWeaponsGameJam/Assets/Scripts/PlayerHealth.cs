using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            healthBar.value -= 1;
            source.PlayOneShot(source.clip);
        }
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "EnemyBullet")
        {
            healthBar.value -= 1;
            source.PlayOneShot(source.clip);
        }
    }

}
