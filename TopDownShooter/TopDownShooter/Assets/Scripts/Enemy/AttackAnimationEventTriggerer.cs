using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationEventTriggerer : MonoBehaviour
{
    MeleeEnemyManager meleeEnemyManager;
    private void Start()
    {
        meleeEnemyManager = gameObject.GetComponentInParent<MeleeEnemyManager>();
    }
    public void Attack()
    {
        if (meleeEnemyManager != null)
        {
            meleeEnemyManager.Attack();
        }
    }
}
