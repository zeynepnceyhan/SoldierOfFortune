using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    public Joystick joystick;
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint; 
    public float bulletSpeed = 20f;
    public float fireRate = 0.5f;

    private Animator animator;
    private List<GameObject> zombiesInRange = new List<GameObject>(); 
    private float nextFireTime = 0f;
    

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        RotateTowardsNearestZombie();
        RotateBulletSpawnPoint();
        HandleShooting();
    }

    private void HandleMovement()
    {
        var horizontalInput = joystick.Horizontal;
        var verticalInput = joystick.Vertical;

        var movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        var inputMagnitude = movementDirection.magnitude; 

        if (inputMagnitude > 0.1f)
        {
            transform.position += movementDirection.normalized * movementSpeed * inputMagnitude * Time.deltaTime;
            
            var targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            animator.SetFloat(Speed, inputMagnitude);
        }
        else
        {
            animator.SetFloat(Speed, 0);
        }
    }

    private void RotateTowardsNearestZombie()
    {
        if (zombiesInRange.Count > 0)
        {
            var nearestZombie = GetNearestZombie();
            var directionToZombie = (nearestZombie.transform.position - transform.position).normalized;

            directionToZombie.y = 0; 
            if (directionToZombie != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(directionToZombie);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void RotateBulletSpawnPoint()
    {
        if (zombiesInRange.Count > 0)
        {
            var nearestZombie = GetNearestZombie();
            var directionToZombie = (nearestZombie.transform.position - bulletSpawnPoint.position).normalized;
            
            bulletSpawnPoint.rotation = Quaternion.LookRotation(directionToZombie);
        }
    }

    private void HandleShooting()
    {
        if (zombiesInRange.Count > 0 && Time.time >= nextFireTime)
        {
            ShootAtZombie(zombiesInRange[0]);
            nextFireTime = Time.time + fireRate;
        }
    }

    public void ShootAtZombie(GameObject zombie)
    {
        if (!GameManager.Instance.IsPlaying)
        {
            return;
        }
        
        if (zombie != null)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            var rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = (zombie.transform.position - bulletSpawnPoint.position).normalized * bulletSpeed;
            
            zombiesInRange.Remove(zombie);
            
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            zombiesInRange.Remove(other.gameObject);
        }
    }



    public void GameOver()
    {
        if (!GameManager.Instance.IsPlaying)
        {
            return;
        }
        
        GameManager.Instance?.ChangeState(GameManager.GameState.GameOver);
        
        UIManager.Instance?.ShowPanel(GameManager.GameState.GameOver);
        
        enabled = false;
    }

    private GameObject GetNearestZombie()
    {
        var nearestZombie = zombiesInRange[0];
        var shortestDistance = Vector3.Distance(transform.position, nearestZombie.transform.position);

        foreach (GameObject z in zombiesInRange)
        {
            float currentDistance = Vector3.Distance(transform.position, z.transform.position);
            if (currentDistance < shortestDistance)
            {
                nearestZombie = z;
                shortestDistance = currentDistance;
            }
        }
        return nearestZombie;
    }
}
