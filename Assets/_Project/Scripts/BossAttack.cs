using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Impostazioni Attacco")]
    public GameObject projectilePrefab; // Il proiettile del boss
    public Transform firePoint;         // Punto da cui spara (vicino alla bocca/mani)
    public float projectileSpeed = 5f;
    public float fireRate = 2f;         // Spara ogni 2 secondi

    private float nextFireTime;
    private BossFollowMovement movementScript; // Per sapere se il boss si sta muovendo

    void Awake()
    {
        // Recuperiamo lo script del movimento se vogliamo sincronizzare qualcosa
        movementScript = GetComponent<BossFollowMovement>();
    }

    void Update()
    {
        // Controllo del tempo per sparare
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        // Crea il proiettile del boss
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        
        // Lo spara verso il basso (Vector2.down)
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.down * projectileSpeed;
        }

        // Se vuoi che il Boss guardi il giocatore mentre spara, 
        // lo script BossFollowMovement lo sta già facendo col Flip!
    }
}