using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Impostazioni Fisiche")]
    public float speed = 5f;
    public float bulletSpeed = 10f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Riferimenti Oggetti")]
    public SpriteRenderer spriteRenderer;
    public GameObject starPrefab;
    public Transform firePoint;

    [Header("Animazioni (Trascina gli Sprite qui)")]
    public Sprite[] idleSprites;
    public Sprite[] walkSprites;
    public Sprite[] attackSprites;
    public float animationFPS = 12f;
    public float attackDuration = 0.3f; // Durata della posa di attacco

    private float animTimer;
    private int currentFrame;
    private bool isAttacking;
    private float attackTimer;
    private Sprite[] lastArray;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // --- INPUT SYSTEM ---
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    // --- LOGICA ---
    void Update()
    {
        HandleAnimations();
    }

    void FixedUpdate()
    {
        // Movimento
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

        // Flip
        if (moveInput.x > 0.01f) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput.x < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Shoot()
    {
        if (starPrefab != null && firePoint != null)
        {
            // 1. Spariamo la stella
            GameObject star = Instantiate(starPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D starRb = star.GetComponent<Rigidbody2D>();
            if (starRb != null) starRb.linearVelocity = Vector2.up * bulletSpeed;

            // 2. TRIGGERIAMO L'ANIMAZIONE
            TriggerAttackAnimation();
        }
    }

    public void TriggerAttackAnimation()
    {
        isAttacking = true;
        attackTimer = attackDuration;
        currentFrame = 0;
        animTimer = 0f;
    }

    void HandleAnimations()
    {
        Sprite[] currentArray;

        // PRIORITÀ 1: ATTACCO
        if (isAttacking)
        {
            currentArray = attackSprites;
            attackTimer -= Time.deltaTime;
            
            if (attackTimer <= 0) 
            {
                isAttacking = false;
            }
        }
        // PRIORITÀ 2: MOVIMENTO (solo se non sta attaccando)
        else if (Mathf.Abs(moveInput.x) > 0.1f)
        {
            currentArray = walkSprites;
        }
        // PRIORITÀ 3: IDLE
        else
        {
            currentArray = idleSprites;
        }

        // Sicurezza: se l'array scelto è vuoto, non fare nulla
        if (currentArray == null || currentArray.Length == 0) return;

        // Se l'animazione è cambiata rispetto all'ultimo frame, resetta l'indice
        if (currentArray != lastArray)
        {
            currentFrame = 0;
            animTimer = 0f;
            lastArray = currentArray;
            spriteRenderer.sprite = currentArray[0]; // Applica subito il primo frame
        }

        // Ciclo dei frame in base agli FPS
        animTimer += Time.deltaTime;
        if (animTimer >= 1f / animationFPS)
        {
            animTimer = 0f;
            currentFrame = (currentFrame + 1) % currentArray.Length;
            spriteRenderer.sprite = currentArray[currentFrame];
        }
    }
}