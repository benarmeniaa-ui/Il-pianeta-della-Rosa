using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // 1. Aggiungi questa riga in alto!

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject gameOverUI;

    public int lives = 10;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (gameOverUI != null) gameOverUI.SetActive(false);
    }

    private void Update()
    {
        // 2. Modifica qui per usare il nuovo Input System
        if (isGameOver && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    public void PlayerHit()
    {
        lives--;
        Debug.Log("Vite rimaste: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        if (gameOverUI != null) gameOverUI.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void RestartGame() // Messa pubblica così puoi usarla anche dai bottoni UI
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}