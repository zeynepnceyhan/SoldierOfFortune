using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject zombiePrefab; 
    public Transform spawnPoint; 
    public float levelTime = 180f; 
    private int zombiesToSpawn = 0; 
    private float spawnInterval = 0f; 
    private int zombiesSpawned = 0; 

    void Start()
    {
        SetLevelDifficulty(1); 
        StartCoroutine(SpawnZombies());
    }

    void SetLevelDifficulty(int level)
    {
        if (level == 1)
        {
            zombiesToSpawn = 10;
            spawnInterval = levelTime / zombiesToSpawn; 
        }
        else if (level == 2)
        {
            zombiesToSpawn = 20;
            spawnInterval = levelTime / zombiesToSpawn; 
        }
        else if (level == 3)
        {
            zombiesToSpawn = 60;
            spawnInterval = levelTime / zombiesToSpawn; 
        }
    }

    IEnumerator SpawnZombies()
    {
        while (zombiesSpawned < zombiesToSpawn)
        {
            SpawnZombie();
            zombiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }
        
        GameWon();
    }

    void SpawnZombie()
    {
        Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
    }

    void GameWon()
    {
        Debug.Log("Game Won! Zombies Defeated: " + zombiesSpawned);
    }
}