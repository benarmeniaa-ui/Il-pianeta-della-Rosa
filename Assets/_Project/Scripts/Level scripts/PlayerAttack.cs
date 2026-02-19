using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 8f;
    public float fireRate = 0.5f;

    private float lastShotTime = 0f;
    private PlayerMovement movementScript;

    void Awake()
    {
        movementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Time.time > lastShotTime + fireRate)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 1. Logica Fisica: Crea il proiettile
        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.up * projectileSpeed;

        lastShotTime = Time.time;

        // --- AGGIUNTA AUDIO ---
        // Richiamiamo l'AudioManager per riprodurre il suono dell'attacco
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.playerAttackSound);
        }
        // ----------------------

        // 2. Comunicazione: Animazione
        if (movementScript != null)
        {
            movementScript.TriggerAttackAnimation();
        }
    }
}