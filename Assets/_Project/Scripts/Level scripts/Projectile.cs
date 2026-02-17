using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;           // danno del proiettile
    public float lifetime = 5f;      // tempo prima che si auto-distrugga

    private void Start()
    {
        // distrugge il proiettile dopo un po' se non colpisce niente
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // colpisce il Boss
        if (collision.CompareTag("Enemy"))
        {
            Boss boss = collision.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            Destroy(gameObject); // distrugge il proiettile
        }

        // colpisce il terreno o altri ostacoli (opzionale)
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
