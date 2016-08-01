using UnityEngine;
using System.Collections;

public class EnemyManagement : MonoBehaviour
{
    public Transform myTarget;
    public bool isOnRoute;
    public bool isNavPaused;

    public delegate void GeneralEventHandler();
    public GeneralEventHandler EventEnemyDie;
    public GeneralEventHandler EventEnemyWalking;
    public GeneralEventHandler EventEnemyReachedTarget;
    public GeneralEventHandler EventEnemyLostTarget;
    public GeneralEventHandler EventEnemyAttack;

    public delegate void HealthEventHandler(int amt);
    public HealthEventHandler EventEnemyDeductHealth;

    public delegate void NavTargetEventHandler(Transform targetTransform);
    public NavTargetEventHandler EventEnemySetNavTarget;

    public void callEventEnemyDeductHealth(int amt)
    {
        if (EventEnemyDeductHealth != null)
        {
            EventEnemyDeductHealth(amt);
        }
    }

    public void callEventEnemySetNavTarget(Transform target)
    {
        if (EventEnemySetNavTarget != null)
        {
            EventEnemySetNavTarget(target);
        }

        myTarget = target;
    }

    public void callEventEnemyDie()
    {
        if (EventEnemyDie != null)
        {
            EventEnemyDie();
        }
    }

    public void callEventEnemyWalking()
    {
        if (EventEnemyWalking != null)
        {
            EventEnemyWalking();
        }
    }

    public void callEventEnemyReachedTarget()
    {
        if (EventEnemyReachedTarget != null)
        {
            EventEnemyReachedTarget();
        }
    }

    public void callEventEnemyLostTarget()
    {
        if (EventEnemyLostTarget != null)
        {
            EventEnemyLostTarget();
            Debug.Log("Lost Target");
        }

        myTarget = null;
    }

    public void callEventEnemyAttack()
    {
        if (EventEnemyAttack != null)
        {
            EventEnemyAttack();
        }
    }
}