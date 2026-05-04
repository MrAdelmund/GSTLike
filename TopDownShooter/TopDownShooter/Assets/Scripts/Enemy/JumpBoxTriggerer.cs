using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoxTriggerer : MonoBehaviour
{
    [SerializeField] bool isLeftBox = true;
    [SerializeField] int layerToCollideWith;
    MeleeEnemyManager enemyManager;
    void Start()
    {
        enemyManager = GetComponentInParent<MeleeEnemyManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerToCollideWith)
        {
            enemyManager.JumpBoxTriggered(isLeftBox);
        }
        
    }
}