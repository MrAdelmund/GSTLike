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
    static int rangedEnemySpawnBacklog = 5;
    static int meleeEnemySpawnBacklog = 5;
    static bool spawnEnemies = true;
    bool doSpawnEnemiesCheck = true;
    float spawnTimer = 0;
    Rigidbody2D playerRb;
    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
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
        spawnTimer = 0;
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
        if (meleeEnemySpawnBacklog <= 0 && rangedEnemySpawnBacklog <= 0)
            doSpawnEnemiesCheck = false;
    }
    Vector2 GetSpawnLocation()
    {
        Vector2 spawnPos = transform.position;
        bool spawnSide = (Random.Range(0, 2) == 1) ? true : false; //generates a random bool value for the direction the enemy should be spawned in
        spawnPos = new Vector2(spawnPos.x + (horizontalSpawnDist * ((spawnSide) ? -1 : 1)), spawnPos.y + verticalSpawnDist);
        if (spawnSide && playerRb.velocity.x < 0)
            spawnPos = new Vector2(spawnPos.x * 1.5f, spawnPos.y);
        else if (!spawnSide && playerRb.velocity.x > 0)
            spawnPos = new Vector2(spawnPos.x * 1.5f, spawnPos.y);

        return spawnPos;
    }
    public static void AddToEnemySpawnQueue(int amountOfRangedEnemies, int amountOfMeleeEnemies)
    {
        rangedEnemySpawnBacklog += amountOfRangedEnemies;
        meleeEnemySpawnBacklog += amountOfMeleeEnemies;
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
}
