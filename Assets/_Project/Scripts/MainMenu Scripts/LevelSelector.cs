using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelSelector : MonoBehaviour
{
    [System.Serializable]
    public class LevelGroup
    {
        public GameObject lockedVisual;   // Trascina qui l'oggetto con grafica nera e "???"
        public GameObject unlockedVisual; // Trascina qui l'oggetto con grafica a colori e Nome
    }

    [Header("Riferimenti Livelli")]
    public LevelGroup[] levelGroups; // Configura 8 elementi nell'Inspector

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
        UpdateVisuals();
    }

    void OnEnable() { UpdateVisuals(); }

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

        // SELEZIONE LIVELLO (Tasto E)
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SelectLevel();
        }
    }

    void UpdateTargetPosition() { targetPos = new Vector3(-currentIndex * spaceBetweenPlanets, 0, 0); }

    public void UpdateVisuals()
    {
        // Forza lo sblocco del primo livello
        PlayerPrefs.SetInt("Level_1_Unlocked", 1);
        PlayerPrefs.Save();

        for (int i = 0; i < levelGroups.Length; i++)
        {
            if (levelGroups[i].lockedVisual == null || levelGroups[i].unlockedVisual == null) continue;

            int levelNum = i + 1;
            bool isUnlocked = PlayerPrefs.GetInt("Level_" + levelNum + "_Unlocked", 0) == 1;

            // Spegne uno e accende l'altro
            levelGroups[i].unlockedVisual.SetActive(isUnlocked);
            levelGroups[i].lockedVisual.SetActive(!isUnlocked);
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
            Debug.Log("Accesso negato: Pianeta " + levelToLoad + " bloccato.");
        }
    }
}