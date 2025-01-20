using System;
using UnityEngine;

namespace Player
{
    public class PlayerEnemyDetector : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Zombie"))
            {
                playerController.GameOver();
            }
            
        }
    }
}