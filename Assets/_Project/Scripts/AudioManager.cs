using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sorgente Audio")]
    [SerializeField] private AudioSource sfxSource;

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
        // Singleton: garantisce che ci sia solo un AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Resta attivo anche cambiando scena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Funzione universale per riprodurre un suono
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}