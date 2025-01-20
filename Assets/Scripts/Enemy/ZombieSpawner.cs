using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner Instance;

    public string zombieTag = "Zombie";
    public float spawnInterval = 3f;
    public Transform[] spawnPoints;
    public Transform player;
    private bool _canSpawn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned to ZombieSpawner.");
        }

        GameManager.Instance.OnGameStateChanged += OnGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChange;
    }

    private void OnGameStateChange(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Playing)
        {
            _canSpawn = true;
            InvokeRepeating(nameof(SpawnZombie), 0f, spawnInterval);
        }
        else
        {
            _canSpawn = false;
        }
    }


    private void SpawnZombie()
    {
        if (!_canSpawn)
        {
            return;
        }

        if (spawnPoints.Length == 0) return;

        var randomIndex = Random.Range(0, spawnPoints.Length);
        var selectedSpawnPoint = spawnPoints[randomIndex];

        if (selectedSpawnPoint == null)
        {
            Debug.LogWarning("Spawn point is null.");
            return;
        }

        var spawnPosition = selectedSpawnPoint.position;
        var spawnRotation = selectedSpawnPoint.rotation;

        var zombie = ObjectPooler.Instance?.SpawnFromPool(zombieTag, spawnPosition, spawnRotation);
        if (zombie == null)
        {
            Debug.LogError("Failed to spawn zombie from pool.");
            return;
        }

        var enemyAi = zombie.GetComponent<EnemyAI>();
        if (enemyAi != null)
        {
            if (player != null)
            {
                enemyAi.player = player;
            }
            else
            {
                Debug.LogWarning("Player reference is not set in the ZombieSpawner.");
            }
        }
    }

    public void StartLevel(int level)
    {
        spawnInterval = Mathf.Max(1f, spawnInterval - level * 0.5f);
    }
}