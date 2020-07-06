using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    // creating a singleton class
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {       
        // gettting the type of this class (as in MusicPlayer)
        // if there already is one
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // then destroy the one that will be created
            Object.Destroy(gameObject);
        }
        // if there is not already one
        else
        {
            // don't destroy this  MusicPlayer so that the music doesn't restart
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
