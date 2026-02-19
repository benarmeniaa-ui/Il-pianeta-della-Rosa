using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    [System.Serializable]
    public class PlanetSprites
    {
        public string planetName;    // Es: "Marte"
        public Sprite unlockedPhoto; 
        public Sprite lockedPhoto;   
        public TextMeshProUGUI nameText; 
    }

    [Header("Riferimenti")]
    public RectTransform container; 
    public Image[] planetUIElements; 

    [Header("Impostazioni Grafiche")]
    public PlanetSprites[] planetGallery; 
    
    [Header("Movimento")]
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

        for (int i = 0; i < planetUIElements.Length; i++)
        {
            int levelNum = i + 1;
            bool isUnlocked = PlayerPrefs.GetInt("Level_" + levelNum + "_Unlocked", 0) == 1;

            if (isUnlocked)
            {
                planetUIElements[i].sprite = planetGallery[i].unlockedPhoto;
                if(planetGallery[i].nameText != null) planetGallery[i].nameText.text = planetGallery[i].planetName;
            }
            else
            {
                planetUIElements[i].sprite = planetGallery[i].lockedPhoto;
                if(planetGallery[i].nameText != null) planetGallery[i].nameText.text = "???";
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
            // Carica la scena basandosi sul nome "Livello" + numero (es: Livello1, Livello2)
            SceneManager.LoadScene("Livello" + levelToLoad);
        }
        else
        {
            Debug.Log("Pianeta " + levelToLoad + " è ancora bloccato!");
        }
    }
}