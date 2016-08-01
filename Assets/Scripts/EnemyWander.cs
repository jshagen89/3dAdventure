using UnityEngine;
using System.Collections;

public class EnemyWander : MonoBehaviour
{
    private const float LOW_RANGE = 0.3f;
    private const float HIGH_RANGE = 0.4f;
    private const float WANDER_RANGE = 10;

    private EnemyManagement enemyMgmt;
    private NavMeshAgent myNavMesh;
    private float nextCheck;
    private float checkRate;
    private Transform myTransform;
    private NavMeshHit navHit;
    private Vector3 wanderDirection;

    private void OnEnable()
    {
        enemyMgmt = GetComponent<EnemyManagement>();
        myNavMesh = GetComponent<NavMeshAgent>();
        checkRate = Random.Range(LOW_RANGE, HIGH_RANGE);
        myTransform = transform;
        enemyMgmt.EventEnemyDie += disableScript;
    }

    private void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            checkIfShouldWander();
        }
    }

    // Determine if enemy should wander around
    private void checkIfShouldWander()
    {
        if (enemyMgmt.myTarget == null && !enemyMgmt.isOnRoute && !enemyMgmt.isNavPaused)
        {
            if (randomWanderTarget(myTransform.position, WANDER_RANGE, out wanderDirection))
            {
                myNavMesh.SetDestination(wanderDirection);
                enemyMgmt.isOnRoute = true;
                enemyMgmt.callEventEnemyWalking();
            }
        }
    }

    // choose random location in unit sphere to walk to...if not acceptable, choose center of sphere
    private bool randomWanderTarget(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * WANDER_RANGE;
        if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = center;
            return false;
        }
    }

    private void disableScript()
    {
        this.enabled = false;
    }

    private void OnDisable()
    {
        enemyMgmt.EventEnemyDie -= disableScript;
    }
}
