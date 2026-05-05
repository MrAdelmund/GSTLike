using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEventTriggererRangedEnemy : MonoBehaviour
{
    RangedEnemyManager rangedEnemyManager;
    private void Start()
    {
        rangedEnemyManager = gameObject.GetComponentInParent<RangedEnemyManager>();
    }
    public void Attack()
    {
        if (rangedEnemyManager != null)
        {
            rangedEnemyManager.Attack();
        }
    }
}
