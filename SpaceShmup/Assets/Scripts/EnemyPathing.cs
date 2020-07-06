using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // got the waveconfig prefab
    // the instance variable
    WaveConfig waveConfig;
    // list of waypoints, just need the transform
    // create an empty list for the waypoints
    List<Transform> waypoints;
    float moveSpeed = 2f;
    int waypointIndex = 0;
        
    // Start is called before the first frame update
    void Start()
    {
        // run the public method on waveconfig to return a list of waypoints
        waypoints = waveConfig.GetWaypoints();

        // the EnemyPathing script is on Enemy, so the transform.position is that of the enemy
        // this gets the position of the waypoint list transform object
        // the enemy transform postion is a reference to the above
        // the first postition the enemy is set to is 0 in the list
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        // the class instance waveConfig variable (above void start)
        // the class instance var is set to the waveConfig variable passed into the method
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        // if the waypoint index is less than the amount of waypoints in the list
        // i.e if we haven't yet reached the last waypoint
        if (waypointIndex <= waypoints.Count - 1)
        {
            // get the position of the current waypoint
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMovementSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            // increment the waypointIndex 
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }
}
