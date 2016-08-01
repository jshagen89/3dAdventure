using UnityEngine;
using System.Collections;

public class EnemyPursue : MonoBehaviour
{
    private const float LOW_RATE = 0.1f;
    private const float HIGH_RATE = 0.2f;

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
            attemptPursuit();
        }
    }

    private void attemptPursuit()
    {
        if (!enemyMgmt.isNavPaused && enemyMgmt.myTarget != null)
        {
            myNavMesh.SetDestination(enemyMgmt.myTarget.position);

            if (myNavMesh.remainingDistance > myNavMesh.stoppingDistance)
            {
                enemyMgmt.callEventEnemyWalking();
                enemyMgmt.isOnRoute = true;
            }
        }
    }

    private void disableScript()
    {
        myNavMesh.enabled = false;
        this.enabled = false;                               // disables script when enemy dies
    }

    private void OnDisable()
    {
        enemyMgmt.EventEnemyDie -= disableScript;
    }
}
