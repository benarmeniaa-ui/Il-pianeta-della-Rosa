using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Impostazioni Fisiche")]
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Riferimenti")]
    public SpriteRenderer spriteRenderer;

    [Header("Animazioni (No Animator)")]
    public Sprite[] idleSprites;
    public Sprite[] walkSprites;
    public Sprite[] attackSprites;
    public float animationFPS = 12f;
    public float attackDuration = 0.3f; 

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

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // FUNZIONE PUBBLICA: Questa verrà chiamata dallo script PlayerAttack
    public void TriggerAttackAnimation()
    {
        isAttacking = true;
        attackTimer = attackDuration;
        currentFrame = 0;
        animTimer = 0f;
    }

    void Update()
    {
        HandleAnimations();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

        if (moveInput.x > 0.01f) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput.x < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
    }

    void HandleAnimations()
    {
        Sprite[] currentArray;

        if (isAttacking)
        {
            currentArray = attackSprites;
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0) isAttacking = false;
        }
        else if (Mathf.Abs(moveInput.x) > 0.1f)
        {
            currentArray = walkSprites;
        }
        else
        {
            currentArray = idleSprites;
        }

        if (currentArray == null || currentArray.Length == 0) return;

        if (currentArray != lastArray)
        {
            currentFrame = 0;
            animTimer = 0f;
            lastArray = currentArray;
        }

        animTimer += Time.deltaTime;
        if (animTimer >= 1f / animationFPS)
        {
            animTimer = 0f;
            currentFrame = (currentFrame + 1) % currentArray.Length;
            spriteRenderer.sprite = currentArray[currentFrame];
        }
    }
}