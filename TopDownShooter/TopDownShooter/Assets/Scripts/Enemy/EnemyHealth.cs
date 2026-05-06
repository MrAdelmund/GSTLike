using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 100;
    [SerializeField] GameObject deathEffectPrefab;
    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        if (enemyHealth < 0)
        {
            Instantiate(deathEffectPrefab,transform.position,Quaternion.Euler(Vector3.zero));
            Destroy(gameObject);
        }
    }
}
