using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sorgente Audio")]
    [SerializeField] private AudioSource sfxSource;

    [Range(0f, 1f)] 
    public float globalVolume = 0.5f;

    [Header("Suoni Attacco")]
    public AudioClip playerAttackSound;
    public AudioClip bossAttackSound;
    public AudioClip satelliteAttackSound; // <-- NUOVO SLOT

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
            if (sfxSource != null) sfxSource.volume = globalVolume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, globalVolume);
        }
    }

    public void SetVolume(float volume)
    {
        globalVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
        {
            sfxSource.volume = globalVolume;
        }
    }
}