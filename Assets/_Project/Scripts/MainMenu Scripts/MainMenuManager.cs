using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pannelli UI")]
    public GameObject mainButtonsPanel;
    public GameObject levelSelectPanel;
    public GameObject creditsPanel;

    [Header("Riferimenti Esterni")]
    public LevelSelector levelSelector; 

    void Start()
    {
        ShowMainButtons();
    }

    // --- NUOVA FUNZIONE PER CARICARE QUALSIASI LIVELLO ---
    // Questa funzione può essere usata dai pulsanti dei pianeti
    public void LoadLevel(int sceneIndex)
    {
        // Controlliamo se il livello è sbloccato prima di caricarlo (opzionale ma consigliato)
        // Se è il livello 1 (index 1), parte sempre.
        // Se è il livello 2 (index 2), controlliamo i PlayerPrefs.
        if (sceneIndex == 1 || PlayerPrefs.GetInt("Level_" + sceneIndex + "_Unlocked", 0) == 1)
        {
            Time.timeScale = 1f; // Assicuriamoci che il tempo scorra
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.Log("Il livello " + sceneIndex + " è ancora bloccato!");
            // Qui potresti far partire un suono di "errore" dall'AudioManager
            if (AudioManager.Instance != null) 
                AudioManager.Instance.PlaySound(AudioManager.Instance.buttonClickSound);
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Progressi resettati!");

        if (levelSelector != null)
        {
            levelSelector.UpdateVisuals();
        }
    }

    // --- FUNZIONI ESISTENTI ---
    public void StartGame() { LoadLevel(1); } // Ora usa la nuova funzione

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