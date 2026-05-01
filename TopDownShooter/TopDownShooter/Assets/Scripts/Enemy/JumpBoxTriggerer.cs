using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoxTriggerer : MonoBehaviour
{
    [SerializeField] bool isLeftBox = true;
    EnemyManager enemyManager;
    void Start()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyManager.JumpBoxTriggered(isLeftBox);
    }
}