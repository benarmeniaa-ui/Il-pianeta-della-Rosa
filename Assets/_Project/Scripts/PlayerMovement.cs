using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // Movimento orizzontale
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

        // Aggiorna animazione
        bool isMoving = Mathf.Abs(moveInput.x) > 0.01f;
        animator.SetBool("isMoving", isMoving);

        // Flip personaggio
        if (moveInput.x > 0.01f) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput.x < -0.01f) transform.localScale = new Vector3(-1, 1, 1);

        // Debug
        Debug.Log("MoveInput X: " + moveInput.x + " | isMoving: " + animator.GetBool("isMoving"));
    }
}
