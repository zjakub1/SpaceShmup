using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


// this class is added to the enemy object
// it stores the health
// it has a ontriggerenter2d method that checks to see if it has been hit
// the projectile is set to istrigger so that a hit will register with this gameobject
public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 200;
    [SerializeField] int scoreValue = 10500;


    [Header("Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyShot;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Sound Effects")]
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip enemyLaserSFX;
    [SerializeField] AudioClip enemyDeathSFX;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] [Range(0, 1)] float laserSoundVolume = 0.75f;


    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        // want lasers to be fired from the enemies at relatively random times
        // so do it in between the min and max
        // when each enemy object is created they should have different shotcounter values
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);        
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShots();
    }

    private void CountDownAndShots()
    {
        // deltaTime is the time between the last frame and the current frame
        // this is dependent on the speed of the computer
        // 60 frames/s 0.016 
        // 30 frames/s 0.033 
        // so if shotCounter is 1 then 0.016 will need to run 60 times to decrement to 0
        // 0.033 will need to run 30 times to decrement to 0
        shotCounter -= Time.deltaTime;
        
        if (shotCounter <= 0)
        {
            Fire();
            // need tp restet the shotcounter with a random range so that it doesn't fire continuously
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(enemyShot,transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(enemyLaserSFX, Camera.main.transform.position, laserSoundVolume);
        // need to make the projectile move, so setting the velocity on the rigidbody
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // the commented out if is another way of ensuring that the player laser only deals damage to the enemies
        // the current method is by using layers and chaning the layer matix in edit > project settings
        //if (other.CompareTag("PlayerLaser"))
        //{
            // the "other" thing that bumped into us (ie player laser)
            //type has upper case, variable has lower case
            // creating a variable because we want to get the damagedealer from the thing that bumped into us
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            // this protects against a null value condition
            if (!damageDealer) { return;  }
            ProcessHit(damageDealer);
        //}
    }

    private void ProcessHit(DamageDealer damageDealer)
    {        
        health -= damageDealer.GetDamage();
        // destroy the gameobject that the damagedealer object is attached to
        // in this case the player laser will be destroyed
        damageDealer.Hit();
        if (health <= 0)
        {            
            Die();            
        }
    }

    private void Die()
    {
        Object.Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
       
        //play the sound at the camera position, otherwise there will be a drop off in volume due to it's position
        AudioSource.PlayClipAtPoint(enemyDeathSFX,Camera.main.transform.position, deathSoundVolume);
        
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity) as GameObject;
        Object.Destroy(explosion, delayTime);

    }
}
