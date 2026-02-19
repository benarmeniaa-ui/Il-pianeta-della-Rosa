using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelSelector : MonoBehaviour
{
    [System.Serializable]
    public class LevelGroup
    {
        public string planetNameDebug;    // Scrivi qui il nome (es: "Marte") per riconoscerlo nel Log
        public GameObject lockedVisual;   // L'oggetto grigio/nero con "???"
        public GameObject unlockedVisual; // L'oggetto a colori con il nome vero
    }

    [Header("Riferimenti Livelli")]
    public LevelGroup[] levelGroups; 

    [Header("Movimento")]
    public RectTransform container; 
    public float spaceBetweenPlanets = 1000f; 
    public float lerpSpeed = 10f;             
    public int totalLevels = 8;

    private int currentIndex = 0;
    private Vector3 targetPos;

    void Start()
    {
        targetPos = container.localPosition;
        // Diamo un piccolo ritardo per assicurarci che Unity abbia caricato tutto
        Invoke("UpdateVisuals", 0.1f);
    }

    void OnEnable() 
    { 
        UpdateVisuals(); 
    }

    void Update()
    {
        // Navigazione A/D
        if (Keyboard.current.dKey.wasPressedThisFrame && currentIndex < totalLevels - 1)
        {
            currentIndex++;
            UpdateTargetPosition();
        }
        else if (Keyboard.current.aKey.wasPressedThisFrame && currentIndex > 0)
        {
            currentIndex--;
            UpdateTargetPosition();
        }

        container.localPosition = Vector3.Lerp(container.localPosition, targetPos, Time.deltaTime * lerpSpeed);

        // Selezione (Tasto E)
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SelectLevel();
        }
    }

    void UpdateTargetPosition() 
    { 
        targetPos = new Vector3(-currentIndex * spaceBetweenPlanets, 0, 0); 
    }

    public void UpdateVisuals()
    {
        // Forza lo sblocco del primo livello nei salvataggi
        PlayerPrefs.SetInt("Level_1_Unlocked", 1);
        PlayerPrefs.Save();

        for (int i = 0; i < levelGroups.Length; i++)
        {
            // Salta se mancano gli oggetti nell'Inspector per evitare errori
            if (levelGroups[i].lockedVisual == null || levelGroups[i].unlockedVisual == null) 
            {
                continue;
            }

            int levelNum = i + 1;
            bool isUnlocked = PlayerPrefs.GetInt("Level_" + levelNum + "_Unlocked", 0) == 1;

            // --- LOG DI CONTROLLO ---
            // Se vedi questo in rosso nella Console, il livello è bloccato
            string logColor = isUnlocked ? "green" : "red";
            Debug.Log($"<color={logColor}>LIVELLO {levelNum} ({levelGroups[i].planetNameDebug}): Sbloccato = {isUnlocked}</color>");

            // SPEGNIMENTO FORZATO: Reset dello stato degli oggetti
            levelGroups[i].unlockedVisual.SetActive(false);
            levelGroups[i].lockedVisual.SetActive(false);

            // ATTIVAZIONE: Accende solo l'oggetto corretto
            if (isUnlocked)
            {
                levelGroups[i].unlockedVisual.SetActive(true);
            }
            else
            {
                levelGroups[i].lockedVisual.SetActive(true);
            }
        }
    }

    void SelectLevel()
    {
        int levelToLoad = currentIndex + 1;
        bool isUnlocked = PlayerPrefs.GetInt("Level_" + levelToLoad + "_Unlocked", 0) == 1;

        if (isUnlocked)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Livello" + levelToLoad);
        }
        else
        {
            Debug.Log("<color=white>ACCESSO NEGATO: Pianeta bloccato!</color>");
        }
    }
}