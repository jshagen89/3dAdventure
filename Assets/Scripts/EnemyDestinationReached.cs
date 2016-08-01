using UnityEngine;
using System.Collections;

public class EnemyDestinationReached : MonoBehaviour
{
    private const float LOW_RATE = 0.3f;
    private const float HIGH_RATE = 0.4f;

    private EnemyManagement enemyMgmt;
    private NavMeshAgent myNavMesh;
    private float checkRate;
    private float nextCheck;

    private void OnEnable()
    {
        enemyMgmt = GetComponent<EnemyManagement>();
        myNavMesh = GetComponent<NavMeshAgent>();
        checkRate = Random.Range(LOW_RATE, HIGH_RATE);
        enemyMgmt.EventEnemyDie += disableScript;
    }

    private void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            isDestinationReached();
        }
    }

    private void isDestinationReached()
    {
        if (enemyMgmt.isOnRoute)
        {
            if (myNavMesh.remainingDistance < myNavMesh.stoppingDistance)
            {
                enemyMgmt.isOnRoute = false;
                enemyMgmt.callEventEnemyReachedTarget();
            }
        }
    }

    private void disableScript()
    {
        this.enabled = false;                               // disables script when enemy dies
    }

    private void OnDisable()
    {
        enemyMgmt.EventEnemyDie -= disableScript;
    }
}
