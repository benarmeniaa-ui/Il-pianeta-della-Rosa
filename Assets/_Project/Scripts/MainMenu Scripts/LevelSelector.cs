using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelSelector : MonoBehaviour
{
    [Header("Riferimenti")]
    public RectTransform container; // Trascina qui il LevelContainer
    
    [Header("Impostazioni")]
    public float spaceBetweenPlanets = 1000f; // La distanza esatta tra un pianeta e l'altro
    public float lerpSpeed = 10f;             // Velocità della transizione fluida
    public int totalLevels = 8;

    private int currentIndex = 0;
    private Vector3 targetPos;

    void Start()
    {
        // Posizione iniziale (Pianeta 1 al centro)
        targetPos = container.localPosition;
    }

    void Update()
    {
        // Spostamento tra i pianeti (TAC TAC)
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

        // Movimento fluido verso la posizione target
        container.localPosition = Vector3.Lerp(container.localPosition, targetPos, Time.deltaTime * lerpSpeed);

        // Selezione con E
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SelectLevel();
        }
    }

    void UpdateTargetPosition()
    {
        // Se il pianeta 1 è a 0 e il pianeta 2 è a 1000, 
        // per centrare il pianeta 2 dobbiamo muovere il container a -1000.
        targetPos = new Vector3(-currentIndex * spaceBetweenPlanets, 0, 0);
    }

    void SelectLevel()
    {
        // Carica i livelli (al momento solo i primi 2)
        if (currentIndex == 0) SceneManager.LoadScene("Livello1");
        else if (currentIndex == 1) SceneManager.LoadScene("Livello2");
        else Debug.Log("Pianeta " + (currentIndex + 1) + " non ancora esplorabile!");
    }
}