using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject victoryUI; 

    [Header("Game Stats")]
    public int lives = 10;
    private bool isGameOver = false;
    private bool isVictory = false;

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
        if (victoryUI != null) victoryUI.SetActive(false);
    }

    private void Update()
    {
        if (isGameOver && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    public void PlayerHit()
    {
        if (isVictory) return; 

        lives--;
        if (lives <= 0) GameOver();
    }

    private void GameOver()
    {
        isGameOver = true;
        if (gameOverUI != null) gameOverUI.SetActive(true);
        Time.timeScale = 0f; 
    }

    // --- GESTIONE VITTORIA ---
    public void WinGame()
    {
        if (isGameOver || isVictory) return; 
        
        isVictory = true;

        // --- LOGICA DI SBLOCCO PER IL PROTOTIPO ---
        // Otteniamo l'indice della scena attuale
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Se abbiamo vinto il Livello 1 (Index 1), sblocchiamo il Livello 2
        if (currentSceneIndex == 1)
        {
            PlayerPrefs.SetInt("Level_2_Unlocked", 1);
            PlayerPrefs.Save();
            Debug.Log("Livello 2 Sbloccato con successo!");
        }
        // ------------------------------------------

        if (victoryUI != null) victoryUI.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Nessun altro livello trovato! Torno al menu.");
            SceneManager.LoadScene(0); 
        }
    }
}