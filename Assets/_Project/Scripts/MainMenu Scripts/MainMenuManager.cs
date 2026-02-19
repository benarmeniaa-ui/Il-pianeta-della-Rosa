using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pannelli UI")]
    public GameObject mainButtonsPanel;
    public GameObject levelSelectPanel;
    public GameObject creditsPanel;

    [Header("Riferimenti Esterni")]
    public LevelSelector levelSelector; // Trascina qui l'oggetto che ha lo script LevelSelector

    void Start()
    {
        ShowMainButtons();
    }

    // --- NUOVA FUNZIONE RESET ---
    public void ResetProgress()
    {
        // Cancella tutti i salvataggi (livelli sbloccati)
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        Debug.Log("Progressi resettati!");

        // Se il LevelSelector è assegnato, forza l'aggiornamento visivo dei pianeti
        if (levelSelector != null)
        {
            levelSelector.UpdateVisuals();
        }
    }

    // --- FUNZIONI ESISTENTI ---
    public void StartGame() { SceneManager.LoadScene(1); }

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
        Debug.Log("Uscita...");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}