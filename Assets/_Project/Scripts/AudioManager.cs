using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sorgenti Audio")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource; // Trascina qui la sorgente per la musica

    [Range(0f, 1f)] 
    public float globalVolume = 0.5f;

    // Stati per il Mute
    private bool sfxMuted = false;
    private bool musicMuted = false;

    [Header("Suoni Attacco")]
    public AudioClip playerAttackSound;
    public AudioClip bossAttackSound;
    public AudioClip satelliteAttackSound;

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
            UpdateVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- LOGICA SFX (Effetti) ---
    public void PlaySound(AudioClip clip)
    {
        // Riproduce solo se non è mutato
        if (clip != null && sfxSource != null && !sfxMuted)
        {
            sfxSource.PlayOneShot(clip, globalVolume);
        }
    }

    public void ToggleSFX()
    {
        sfxMuted = !sfxMuted;
        // Se sfxSource gestisce anche suoni continui, usiamo il mute
        sfxSource.mute = sfxMuted; 
        Debug.Log("SFX Muted: " + sfxMuted);
    }

    // --- LOGICA MUSICA ---
    public void ToggleMusic()
    {
        musicMuted = !musicMuted;
        if (musicSource != null)
        {
            musicSource.mute = musicMuted;
        }
        Debug.Log("Music Muted: " + musicMuted);
    }

    // --- VOLUME GENERALE ---
    public void SetVolume(float volume)
    {
        globalVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        if (sfxSource != null) sfxSource.volume = globalVolume;
        if (musicSource != null) musicSource.volume = globalVolume;
    }
}