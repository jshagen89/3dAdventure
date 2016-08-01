using UnityEngine;
using System.Collections;

public class EnemyDetection : MonoBehaviour
{
    private const int CHECK_RADIUS = 80;
    private const float LOW_RATE = 0.8f;
    private const float HIGH_RATE = 1.2f;

    private EnemyManagement enemyMgmt;
    private Transform myTransform;
    public Transform head;
    public LayerMask playerLayer;
    public LayerMask sightLayer;
    private float checkRate;
    private float nextCheck;
    private RaycastHit hit;

    private void OnEnable()
    {
        enemyMgmt = GetComponent<EnemyManagement>();
        myTransform = transform;
        checkRate = Random.Range(LOW_RATE, HIGH_RATE);      // enemies have different check rates
        enemyMgmt.EventEnemyDie += disableDetection;
    }

    private void Update()
    {
        performDetection();
    }

    private void performDetection()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            Collider[] colliders = Physics.OverlapSphere(myTransform.position, CHECK_RADIUS, playerLayer);

            if (colliders.Length > 0)
            {
                foreach (Collider col in colliders)
                {
                    if (col.CompareTag("Player"))
                    {
                        if (canTargetBeSeen(col.transform))
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                enemyMgmt.callEventEnemyLostTarget();
            }
        }
    }

    private bool canTargetBeSeen(Transform target)
    {
        if (Physics.Linecast(head.position, target.position, out hit, sightLayer))
        {
            if (hit.transform == target)
            {
                enemyMgmt.callEventEnemySetNavTarget(target);
                return true;
            }
            else
            {
                enemyMgmt.callEventEnemyLostTarget();
                return false;
            }
        }
        else
        {
            enemyMgmt.callEventEnemyLostTarget();
            return false;
        }
    }

    private void disableDetection()
    {
        this.enabled = false;                       // stops Update from running after enemy dies
    }

    private void OnDisable()
    {
        enemyMgmt.EventEnemyDie -= disableDetection;
    }
}
