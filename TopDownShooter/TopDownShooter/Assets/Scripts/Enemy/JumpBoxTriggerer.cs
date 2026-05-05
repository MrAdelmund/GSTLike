using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoxTriggerer : MonoBehaviour
{
    [SerializeField] bool isLeftBox = true;
    [SerializeField] int layerToCollideWith;
    [SerializeField] bool isPartOfMeleeEnemy = true;
    [SerializeField] bool isPartOfRangedEnemy = false;
    MeleeEnemyManager meleeEnemyManager;
    RangedEnemyManager rangedEnemyManager;
    private void Start()
    {
        if (isPartOfMeleeEnemy)
            meleeEnemyManager = GetComponentInParent<MeleeEnemyManager>();
        if (isPartOfRangedEnemy)
            rangedEnemyManager = GetComponentInParent<RangedEnemyManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerToCollideWith)
        {
            if (isPartOfMeleeEnemy)
                meleeEnemyManager.JumpBoxTriggered(isLeftBox);
            else
                rangedEnemyManager.JumpBoxTriggered(isLeftBox);
        }
    }
}