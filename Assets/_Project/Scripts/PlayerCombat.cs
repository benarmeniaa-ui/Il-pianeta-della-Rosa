using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public GameObject starPrefab;
    public Transform firePoint;
    public float attackCooldown = 0.5f;

    private float attackTimer;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
    }

    // Questa funzione verrà chiamata dall'Input System
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && attackTimer >= attackCooldown)
        {
            animator.SetTrigger("Attack");
            attackTimer = 0f;
        }
    }

    public void SpawnStar()
    {
        Instantiate(starPrefab, firePoint.position, Quaternion.identity);
    }

    public void StartAttack()
    {
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.isAttacking = true;
    }

    public void EndAttack()
    {
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.isAttacking = false;
    }
}
