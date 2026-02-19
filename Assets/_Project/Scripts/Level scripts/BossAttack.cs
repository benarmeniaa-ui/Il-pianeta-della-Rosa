using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Impostazioni Attacco")]
    public GameObject projectilePrefab; 
    public Transform firePoint;         
    public float projectileSpeed = 5f;
    public float fireRate = 2f;         

    private float nextFireTime;
    private BossFollowMovement movementScript; 

    void Awake()
    {
        movementScript = GetComponent<BossFollowMovement>();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        // --- AGGIUNTA AUDIO ---
        // Richiama il suono specifico dell'attacco del Boss
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.bossAttackSound);
        }
        // ----------------------

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.down * projectileSpeed;
        }
    }
}