//using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class Player : MonoBehaviour
{
    // variable
    [Header("Player")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float padding = 0.01f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Sound")]
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip playerLaserSFX;
    [SerializeField] [Range(0, 1)] float laserSoundVolume = 0.25f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;


    //float time = 0;
    //int frames = 0;



    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

        // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        /*
        time += Time.deltaTime;
        frames++;
        Debug.Log("t: "+  time);
        Debug.Log("dt: " + Time.deltaTime);
        Debug.Log("f: " + frames);
        Debug.Log("tt: " + Time.time);
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // the commented out if is another way of ensuring that the player laser only deals damage to the enemies
        // the current method is by using layers and chaning the layer matix in edit > project settings
        //if (collision.CompareTag("EnemyLaser"))
        //{    
        Debug.Log("OnTriggerEvent");
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            // if there is no damagedealer on the collision object 
            // then return - this protects against null values
            if (!damageDealer) { return;  }
            ProcessHit(damageDealer);        
        //}
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        //Debug.Log("From Damage dealer : " + damageDealer.GetDamage());
        health -= damageDealer.GetDamage();
        // destroy the gameobject that the damagedealer object is attached to
        Debug.Log("Health : " + health);
        // in this case the enemy laser
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("Die 1");
        // call the load game over screen ,which in turn calls a coroutine
        FindObjectOfType<Level>().LoadGameOver();
        // after the coroutine is called and runs in parallel the yield is run, (this starts a delay in the level cs script)   
        // and it immediately runs the below commands
        // when the delay is done the game over screen is also loaded
        Debug.Log("Die 2");
        Object.Destroy(gameObject);
        Debug.Log("Die 3");
        AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position, deathSoundVolume);
        Debug.Log("Die 4");
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // store the coroutine in the handle so we can stop it below if required
            // start the firecontinuously coroutine
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            // stop specific coroutine
            // or could do StopAllCoroutines - but this would stop any coroutines that are running
            StopCoroutine(firingCoroutine);
        }

    }

    IEnumerator FireContinuously()
    {
        // the getbuttondown method returns a value of true
        // this value will be true whilst the player pushed down the fire1 button
        // this will always be true unless there is a condition to stop this routine
        while (true)
        {
            Vector3 laserPadding = new Vector3(0f, 0.5f, 0f);
            AudioSource.PlayClipAtPoint(playerLaserSFX, Camera.main.transform.position, laserSoundVolume);
            // creating a gameobject explicitly casting to GameObject
            GameObject laser = Instantiate(playerLaser, transform.position + laserPadding, Quaternion.identity) as GameObject;
            // set the velocity on the object (remember to either set gravity to 0 or to set the body time to kinematic)
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }        
    }

    private void Move()
    {
        // Time.deltaTime allows the control of the player to be frame rate independent as it's using the time it takes to make a frame to smooth the movement
        // for example 60 (fps) * 1 unit * 0.016 = 1 unit per second
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        
        // use var as the method would return a specific type
        // only allow the player to move within the boundry defined below
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);

        //Debug.Log("deltaX " + deltaX);
        //Debug.Log("Time " + Time.deltaTime);
        //Debug.Log("transform " + transform.position.x + transform.position.y);
        //Debug.Log(transform.position.x + " " + deltaX + " " + Time.deltaTime);
        //Debug.Log("newXPos " + newXPos);

    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        // use camera U to convert to world space 1
        // 0,0 - bottom left 1, 0 - bottom right, 0, 1 -  top left, 1, 1 top right
        // this means that if the camera size changes the boundry would also change automatically
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public int GetHealth()
    {
        return health;
    }

}
