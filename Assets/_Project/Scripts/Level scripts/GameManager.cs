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
        
        // Debug iniziale per confermare le vite all'avvio
        Debug.Log($"<color=cyan>GameManager:</color> Partita iniziata. Vite disponibili: {lives}");
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
        if (isVictory || isGameOver) return; 

        lives--;

        // --- DEBUG E AUDIO ---
        Debug.Log($"<color=red>PLAYER COLPITO!</color> Vite rimaste: <color=yellow>{lives}</color>");
        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.playerHurtSound);
        }
        // ----------------------

        if (lives <= 0) GameOver();
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("<color=black><b>GAME OVER</b></color>");
        if (gameOverUI != null) gameOverUI.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void WinGame()
    {
        if (isGameOver || isVictory) return; 
        
        isVictory = true;
        Debug.Log("<color=green>VITTORIA!</color>");

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Se abbiamo vinto il Livello 1 (Index 1), sblocchiamo il Livello 2
        if (currentSceneIndex == 1)
        {
            PlayerPrefs.SetInt("Level_2_Unlocked", 1);
            PlayerPrefs.Save();
            Debug.Log("<color=magenta>Livello 2 Sbloccato!</color>");
        }

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
            Debug.Log("Torna al menu.");
            SceneManager.LoadScene(0); 
        }
    }
}