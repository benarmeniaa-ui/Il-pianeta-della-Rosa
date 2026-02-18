using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pannelli UI")]
    public GameObject mainButtonsPanel;  // Il pannello con Start, Level Select, ecc.
    public GameObject levelSelectPanel;  // Il pannello con il carosello dei pianeti
    public GameObject creditsPanel;      // Il pannello con i nomi degli autori

    void Start()
    {
        // All'avvio, mostriamo solo i tasti principali
        ShowMainButtons();
    }

    // --- FUNZIONI PER I BOTTONI ---

    public void StartGame()
    {
        // Carica direttamente il Livello 1 (Index 1 nelle Build Settings)
        SceneManager.LoadScene(1);
    }

    public void OpenLevelSelect()
    {
        mainButtonsPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        mainButtonsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ShowMainButtons()
    {
        mainButtonsPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Uscita dal gioco...");

        // Se siamo dentro l'Editor di Unity
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Se il gioco è buildato (versione finale)
            Application.Quit();
        #endif
    }
}