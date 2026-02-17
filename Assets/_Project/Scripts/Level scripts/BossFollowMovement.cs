using UnityEngine;

public class BossFollowMovement : MonoBehaviour
{
    [Header("Riferimenti")]
    public Transform player; // Trascina qui il Player dall'Inspector
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Parametri Movimento")]
    public float bossSpeed = 3f;
    public float stopDistance = 0.5f; // Distanza minima per evitare che il boss "tremi" sopra il player

    [Header("Sprites Boss")]
    public Sprite[] idleSprites;
    public Sprite[] walkSprites;
    public float animationFPS = 10f;

    private float animationTimer;
    private int currentFrame;
    private bool isMoving;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    void HandleMovement()
    {
        if (player == null) return;

        // Calcoliamo la direzione orizzontale verso il player
        float directionX = player.position.x - transform.position.x;

        // Se il boss è abbastanza lontano, si muove
        if (Mathf.Abs(directionX) > stopDistance)
        {
            float moveStep = Mathf.Sign(directionX) * bossSpeed;
            rb.linearVelocity = new Vector2(moveStep, rb.linearVelocity.y);
            isMoving = true;

            // Gira il boss verso il player
            transform.localScale = new Vector3(Mathf.Sign(directionX), 1, 1);
        }
        else
        {
            // Se è vicino, si ferma
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            isMoving = false;
        }
    }

    void HandleAnimation()
    {
        Sprite[] activeArray = isMoving ? walkSprites : idleSprites;

        if (activeArray.Length == 0) return;

        animationTimer += Time.deltaTime;
        if (animationTimer >= 1f / animationFPS)
        {
            animationTimer = 0f;
            currentFrame = (currentFrame + 1) % activeArray.Length;
            spriteRenderer.sprite = activeArray[currentFrame];
        }
    }
}