using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int numberOfEnemies = 5;
    private bool hasSpawned = false;


    public void SpawnEnemies()
    {
        if(hasSpawned) return;

        for(int i = 0; i < numberOfEnemies; i++) 
        {
            int spawnIndex = i % spawnPoints.Length;
            Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        }
        hasSpawned = true;
    }
}
