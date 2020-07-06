using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float movementSpeed = 2f;

    public GameObject GetEnemyPrefab()    { return enemyPrefab; }

    //public GameObject GetPathPrefab()    { return pathPrefab; }
    //This method returns a list of transforms
    public List<Transform> GetWaypoints()
    {
        //assign the (currently empty) list to the variable or does this set a reference?
        var waveWayPoints = new List<Transform>();
        // foreach object variablename in sourceobject
        foreach (Transform child in pathPrefab.transform)
        {
            // for each of the above add the child to the list
            waveWayPoints.Add(child);
        }

        return waveWayPoints;
    }      

    public float GetTimeBetweenSpawns()    { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor()    { return spawnRandomFactor; }
    public int GetNumberOfEnemies()    { return numberOfEnemies; }
    public float GetMovementSpeed()    { return movementSpeed; }

}
