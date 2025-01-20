using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;  
    [SerializeField] private NavMeshAgent agent;
    private GameManager _gameManager;
   
    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        agent.enabled = _gameManager.IsPlaying;

        if (!agent.enabled)
        {
            return;
        }
        
        if (player != null && agent != null)
        {
            if (agent.isOnNavMesh)
            {
                agent.SetDestination(player.position);
            }
        }
    }
}