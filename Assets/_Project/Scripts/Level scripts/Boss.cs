using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 5;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Boss HP: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss sconfitto!");

        // 1. Comunichiamo al GameManager che abbiamo vinto
        if (GameManager.Instance != null)
        {
            GameManager.Instance.WinGame();
        }

        // 2. Disattiviamo il Boss
        gameObject.SetActive(false);
    }
}