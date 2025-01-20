using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    public void DespawnBullet()
    {
        ObjectPooler.Instance.Despawn(gameObject);
    }
}