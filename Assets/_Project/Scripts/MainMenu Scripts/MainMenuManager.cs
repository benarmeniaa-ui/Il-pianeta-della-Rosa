using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainButtonsPanel;
    public GameObject levelSelectPanel;

    public void StartGame() // Collegato al tasto START
    {
        SceneManager.LoadScene(1); // Carica il primo livello
    }

    public void OpenLevelSelect() // Collegato al tasto LEVEL SELECT
    {
        mainButtonsPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void CloseLevelSelect() // Per tornare indietro
    {
        levelSelectPanel.SetActive(false);
        mainButtonsPanel.SetActive(true);
    }

    public void ExitGame() // Collegato al tasto EXIT
    {
        Application.Quit();
    }
}