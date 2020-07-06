using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Spinner : MonoBehaviour
{

    private float multiplyValue = 500f;
    private float spinSpeed;
    private float minSpinSpeed1 = -2f;
    //[SerializeField] float minSpinSpeed2 = -10f;
    private float maxSpinSpeed1 = 2f;
    //[SerializeField] float maxSpinSpeed2 = 10f;

    private void Start()
    {
        //spinSpeed = Random.Range(Random.Range(minSpinSpeed1, minSpinSpeed2), Random.Range(maxSpinSpeed1, maxSpinSpeed2));
        spinSpeed = Random.Range(minSpinSpeed1, maxSpinSpeed1) * multiplyValue;
        //Debug.Log("min speed " + spinSpeed);
        //Debug.Log("max speed " + spinSpeed);
    }

    // Update is called once per frame
    void Update()
    {        
        Debug.Log("speed " + spinSpeed);
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}
 