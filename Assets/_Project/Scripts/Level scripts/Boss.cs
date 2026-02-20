using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public int health = 5;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("Effetto Danno")]
    public Color flashColor = Color.green; // Ora impostato su Verde
    public float flashDuration = 0.1f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) 
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Boss HP: " + health);

        // --- FLASH VISIVO ---
        if (spriteRenderer != null)
        {
            StopAllCoroutines(); 
            StartCoroutine(HitFlash());
        }

        // --- AUDIO ---
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.bossHurtSound);
        }

        if (health <= 0) Die();
    }

    IEnumerator HitFlash()
    {
        // Applica il colore verde per il feedback del danno
        spriteRenderer.color = flashColor; 
        
        yield return new WaitForSeconds(flashDuration);
        
        // Ripristina il colore originale
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.WinGame();
        }
        
        gameObject.SetActive(false);
    }
}