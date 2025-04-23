using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // the 5 different enemy prefabs being used 
    public Transform[] spawnPoints; // the set spawn points being used 
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnEnemies();
            Debug.Log("Enemies spawned into scene");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DespawnEnemies();
            Debug.Log("Enemies have vanished");
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Length && i < enemyPrefabs.Length; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
            spawnedEnemies.Add(enemy);
        }
    }

    private void DespawnEnemies()
    {
        foreach(GameObject enemy in spawnedEnemies)
        {
            if(enemy != null)
            {
                Destroy(enemy);
            }
        }
        spawnedEnemies.Clear();
    }
}
