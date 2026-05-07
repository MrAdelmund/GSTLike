using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTriggerBox : MonoBehaviour
{
    [SerializeField] int amountOfRangedEnemiesToAdd = 5;
    [SerializeField] int amountOfMeleeEnemiesToAdd = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            EnemySpawner.AddToEnemySpawnQueue(amountOfRangedEnemiesToAdd, amountOfMeleeEnemiesToAdd);
    }
}
