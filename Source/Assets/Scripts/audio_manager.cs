using UnityEngine;


public class audio_manager : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] public AudioSource footStep;

    [Header("Clip")]
    public AudioClip background;
    public AudioClip menu;
    public AudioClip explode;
    public AudioClip itemPickup;
    public AudioClip placeBomb;
    public AudioClip playerDeath;
    public AudioClip respawn;
    public AudioClip winGame;
    public AudioClip winLevel;
    public AudioClip buttonClick;
    public AudioClip teleport;

    public static audio_manager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        footStep.enabled = false;
        musicSource.clip = menu;
        musicSource.Play();
    }

    public void changeBackgroundMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
