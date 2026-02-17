using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Controlla se ha colpito il Player
        if (other.CompareTag("Player"))
        {
            // Parla col GameManager per togliere una vita
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerHit();
            }

            Destroy(gameObject); // Distruggi il proiettile
        }
        
        // Si distrugge anche se tocca un muro o il pavimento (opzionale)
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    // Distruggi il proiettile se esce dallo schermo per non appesantire il gioco
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}