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
    private PlayerMovement movementScript; // Riferimento all'altro script

    void Awake()
    {
        // Troviamo lo script di movimento sullo stesso oggetto
        movementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Se usi il nuovo Input System senza Action Map (come nel tuo esempio):
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

        // 2. Comunicazione: Diciamo al PlayerMovement di animare l'attacco
        if (movementScript != null)
        {
            movementScript.TriggerAttackAnimation();
        }
    }
}