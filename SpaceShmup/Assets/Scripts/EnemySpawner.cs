using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    // making Start() a coroutine
    IEnumerator Start()
    {
        // do a thing
        do
        {   
            // start the coroutine and go back to start (yield) and do whatever is after the coroutine once complete - ie the while loop
            yield return StartCoroutine(SpawnAllWaves());
        }
        // if this evauluates to true it jumps back to the start
        while (looping);
    }
     
    private IEnumerator SpawnAllWaves()
    {
        // loop through all our waves
        // initialisation, condition, iteration
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            // get the first wave in the list
            // returns an object of type waveconfig
            var currentWave = waveConfigs[waveIndex];
            // wait for the SpawnAllEnemies CoRoutine to finish before continuing the for loop
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }

    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        var waveEnemyCount = waveConfig.GetNumberOfEnemies();
        // read this https://www.tutorialsteacher.com/csharp/csharp-for-loop
        for (int enemyCount = 0; enemyCount < waveEnemyCount; enemyCount++)
        {
            // Instatiate - what (enemy prefab from the waveconfig), where (the waypaoint from waveconfig - in this case index 0), rotation
            // note : to get the position you need transform.position! not just transform!!!!
            // Quaternion - need to look into but it's sating just yuse the rotation that you have already

            // by creating an newEnemy instance, it allows us to grab a component below
            // we always start at index 0 - because that is the first position in the path
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);

            // for the enemy instance get the pathobject and set the waveconfig - the waveconfig that is local to the SpawnAllEnemies method
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            // wait a certain amount of time before spawning a new enemy
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

}
