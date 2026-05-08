using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float spawnInterval = 0.5f;
    [SerializeField] float horizontalSpawnDist = 12;
    [SerializeField] float verticalSpawnDist = 3;
    [Header("References")]
    [SerializeField] GameObject meleeEnemyPrefab;
    [SerializeField] GameObject rangedEnemyPrefab;
    static int rangedEnemySpawnBacklog = 0;
    static int meleeEnemySpawnBacklog = 0;
    //spawnEnemies bool is for if the spawner is allowed to spawn enemies or not, and is controled by EnemySpawnerBox.
    //It can be used to disable spawning even if the spawn queue has enemies left.
    static bool spawnEnemies = true;
    //doSpawnEnemiesCheck bool stops spawnTimer from being ticked up, this will stop enemies from spawning. 
    //It gets triggered when no enemies are left in the spawn queue.
    static bool doSpawnEnemiesCheck = true;
    float spawnTimer = 0;
    private void Update()
    {
        if (doSpawnEnemiesCheck)
            spawnTimer += Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (spawnEnemies && spawnTimer >= spawnInterval)
            SpawnEnemies();
    }
    void SpawnEnemies()
    {
        //reset timer
        spawnTimer = 0;
        //stops enemy spawning if queue is empty
        if (meleeEnemySpawnBacklog <= 0 && rangedEnemySpawnBacklog <= 0)
            doSpawnEnemiesCheck = false;
        else
        {
            //gets a random enemy to spawn, gives more weight to the larger queue.
            int randomEnemyToSpawn = Random.Range(0, meleeEnemySpawnBacklog + rangedEnemySpawnBacklog);
            if (randomEnemyToSpawn < rangedEnemySpawnBacklog)
            {
                Instantiate(rangedEnemyPrefab, GetSpawnLocation(), Quaternion.Euler(Vector3.zero));
                rangedEnemySpawnBacklog--;
            }else
            {
                Instantiate(meleeEnemyPrefab, GetSpawnLocation(), Quaternion.Euler(Vector3.zero));
                meleeEnemySpawnBacklog--;
            }
        }
    }
    Vector2 GetSpawnLocation()
    {
        Vector2 spawnPos = transform.position;
        bool spawnSide = (Random.Range(0, 2) == 1) ? true : false; //generates a random bool value for the direction the enemy should be spawned in
        spawnPos = new Vector2(spawnPos.x + (horizontalSpawnDist * ((spawnSide) ? -1 : 1)), spawnPos.y + verticalSpawnDist);
        return spawnPos;
    }
    public static void AddToEnemySpawnQueue(int amountOfRangedEnemies, int amountOfMeleeEnemies)
    {
        rangedEnemySpawnBacklog += amountOfRangedEnemies;
        meleeEnemySpawnBacklog += amountOfMeleeEnemies;
        doSpawnEnemiesCheck = true;
    }
    public static void PauseEnemySpawning()
    {
        spawnEnemies = false;
    }
    public static void ResumeEnemySpawning()
    {
        spawnEnemies = true;
    }
    public static void ClearEnemySpawnQueue()
    {
        rangedEnemySpawnBacklog = 0;
        meleeEnemySpawnBacklog = 0;
    }
    //shows the positons of the enemy spawn points in the editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(horizontalSpawnDist, verticalSpawnDist), Vector2.one);
        Gizmos.DrawWireCube(new Vector2(-horizontalSpawnDist, verticalSpawnDist), Vector2.one);
    }
}
