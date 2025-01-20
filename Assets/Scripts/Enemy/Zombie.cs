using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Animator animator; 
    public float destroyDelay = 1.0f; 
    public GameObject bloodEffectPrefab; 
    public int points;

    private bool isHit = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent(out Bullet bullet) && !isHit)
        {
            isHit = true;
            PlayHitAnimation();
            
        }
        
        
        if (other.CompareTag("Bullet") && !isHit)
        {
            isHit = true;
            PlayHitAnimation();
            
        }
        
    }


    private void PlayHitAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Hit"); 
        }
        Invoke(nameof(Die), destroyDelay); 
    }

    private void Die()
    {
        Debug.Log($"Zombie killed, adding points: {points}"); 
        GameManager.Instance.IncreaseScore(points);

        if (bloodEffectPrefab != null)
        {
            Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
        }

        ObjectPooler.Instance.Despawn(gameObject);
    }

}