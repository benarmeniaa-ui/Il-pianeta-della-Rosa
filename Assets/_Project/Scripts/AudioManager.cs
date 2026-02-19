using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sorgente Audio")]
    [SerializeField] private AudioSource sfxSource;

    [Range(0f, 1f)] // Crea una barra trascinabile nell'Inspector
    public float globalVolume = 0.5f;

    [Header("Suoni Attacco")]
    public AudioClip playerAttackSound;
    public AudioClip bossAttackSound;

    [Header("Suoni Danno")]
    public AudioClip playerHurtSound;
    public AudioClip bossHurtSound;

    [Header("Suoni UI")]
    public AudioClip buttonHoverSound;
    public AudioClip buttonClickSound;
    public AudioClip levelSelectSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Applica il volume iniziale alla sorgente
            if (sfxSource != null) sfxSource.volume = globalVolume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Funzione universale per riprodurre un suono
    public void PlaySound(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            // Usiamo il volume globale impostato
            sfxSource.PlayOneShot(clip, globalVolume);
        }
    }

    // FUNZIONE PER LA UI: Collega questa funzione a uno Slider
    public void SetVolume(float volume)
    {
        globalVolume = Mathf.Clamp01(volume); // Assicura che sia tra 0 e 1
        if (sfxSource != null)
        {
            sfxSource.volume = globalVolume;
        }
    }
}