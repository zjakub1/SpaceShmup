using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is added to a projectile object
// it will return damage through a public method
public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int GetDamage()
    {
        Debug.Log("Damage :" + damage);
        return damage;   
    }

    public void Hit()
    {
 
        Destroy(gameObject);
        //Object.Destroy(gameObject);
    }
}
