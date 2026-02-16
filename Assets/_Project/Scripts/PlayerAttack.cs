using UnityEngine;
using UnityEngine.InputSystem; // <- questo è obbligatorio per Keyboard.current

public class PlayerAttack : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Shooting")]
    public GameObject projectilePrefab;   // prefab del proiettile
    public Transform shootPoint;          // punto da cui parte il proiettile
    public float projectileSpeed = 8f;
    public float fireRate = 0.5f;         // tempo minimo tra due colpi

    private float lastShotTime = 0f;

    void Update()
    {
        Move();
        Shoot();
    }

    // Movimento orizzontale
    void Move()
    {
        float horizontal = 0f;

#if ENABLE_LEGACY_INPUT_MANAGER
        horizontal = Input.GetAxisRaw("Horizontal"); // vecchio Input
#else
        // se usi il nuovo Input System, puoi leggere InputSystem invece
        if (Keyboard.current.aKey.isPressed) horizontal = -1f;
        if (Keyboard.current.dKey.isPressed) horizontal = 1f;
#endif

        Vector3 pos = transform.position;
        pos.x += horizontal * speed * Time.deltaTime;
        transform.position = pos;

        // Flip semplice del personaggio
        if (horizontal > 0.01f) transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
    }

    // Sparo
    void Shoot()
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetKey(KeyCode.Space) && Time.time > lastShotTime + fireRate)
#else
        if (Keyboard.current.spaceKey.isPressed && Time.time > lastShotTime + fireRate)
#endif
        {
            GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.up * projectileSpeed;
            lastShotTime = Time.time;
        }
    }
}
