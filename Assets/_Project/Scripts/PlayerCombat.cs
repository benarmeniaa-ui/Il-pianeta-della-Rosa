using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Impostazioni Sparo")]
    public GameObject starPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float attackCooldown = 0.5f;

    private float attackTimer;
    private PlayerMovement playerMovement; // Riferimento allo script di movimento

    void Awake()
    {
        // Recuperiamo lo script di movimento per dirgli quando stiamo attaccando
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Il timer avanza sempre
        attackTimer += Time.deltaTime;
    }

    // Chiamata dall'Input System (Tasto Spazio)
    public void OnAttack(InputAction.CallbackContext context)
    {
        // Se premiamo il tasto E il cooldown è passato
        if (context.performed && attackTimer >= attackCooldown)
        {
            PerformAttack();
            attackTimer = 0f;
        }
    }

    void PerformAttack()
    {
        // 1. Diciamo allo script di movimento che stiamo attaccando 
        // (così lui cambia gli sprite visivi)
        if (playerMovement != null)
        {
            playerMovement.TriggerAttackAnimation(); 
        }

        // 2. Creiamo la stella
        GameObject star = Instantiate(starPrefab, firePoint.position, Quaternion.identity);
        
        // 3. Le diamo velocità verso l'alto
        Rigidbody2D rbStar = star.GetComponent<Rigidbody2D>();
        if (rbStar != null)
        {
            rbStar.linearVelocity = Vector2.up * bulletSpeed;
        }
    }
}