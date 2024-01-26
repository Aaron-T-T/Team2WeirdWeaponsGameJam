using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public int health = 50;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        Debug.Log("Health " + health); 
        if(health <= 0 )
        {
           SceneManager.LoadScene("LoseScreen"); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            healthBar.value -= 1;
            health -= 10;
            source.PlayOneShot(source.clip);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            healthBar.value -= 1;
            health -= 10;
            source.PlayOneShot(source.clip);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "EnemyBullet")
        {
            health -= 10;
            healthBar.value -= 1;
            source.PlayOneShot(source.clip);
        }
    }

}
