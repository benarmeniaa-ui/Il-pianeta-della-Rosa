using UnityEngine;

public class SatelliteDefensive : MonoBehaviour
{
    [Header("Movimento Automatico")]
    public float speed = 3f;
    public float range = 5f; // Quanto si sposta a destra e sinistra dal punto iniziale
    private Vector3 startPos;

    [Header("Impostazioni Sparo")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 4f;
    public float fireInterval = 4f;   // Pausa tra una scarica e l'altra
    public int shotsPerBurst = 5;     // Quanti proiettili spara per ogni scarica
    public float timeBetweenShots = 0.2f; // Velocità della raffica interna

    private float nextBurstTime;

    void Start()
    {
        startPos = transform.position;
        nextBurstTime = Time.time + fireInterval;
    }

    void Update()
    {
        // 1. MOVIMENTO AVANTI E INDIETRO (Orizzontale)
        float x = Mathf.Sin(Time.time * speed) * range;
        transform.position = startPos + new Vector3(x, 0, 0);

        // 2. GESTIONE SPARO A INTERMITTENZA
        if (Time.time >= nextBurstTime)
        {
            StartCoroutine(ShootBurst());
            nextBurstTime = Time.time + fireInterval;
        }
    }

    // Coroutine per sparare i 5 proiettili in rapida successione
    System.Collections.IEnumerator ShootBurst()
    {
        for (int i = 0; i < shotsPerBurst; i++)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        // Suono (opzionale: usa lo stesso del boss o uno dedicato nell'AudioManager)
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.bossAttackSound);
        }

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            rb.linearVelocity = Vector2.down * projectileSpeed;
        }
    }
}