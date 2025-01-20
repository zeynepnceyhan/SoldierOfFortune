using UnityEngine;

namespace Player
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Zombie"))
            {
                playerController.ShootAtZombie(other.gameObject);
            }
            
        }
    }
}