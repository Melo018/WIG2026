
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] private float musicVolume = 0.3f;

    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 1f;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    public float MusicVolume => musicVolume;
    public float SFXVolume => sfxVolume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, musicVolume);
        sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, sfxVolume);

        ApplyMusicVolume();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        ApplyMusicVolume();

        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);

        PlayerPrefs.SetFloat(SFXVolumeKey, sfxVolume);
        PlayerPrefs.Save();
    }

    private void ApplyMusicVolume()
    {
        if (musicSource != null)
            musicSource.volume = musicVolume;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null)
            return;

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayRandomSFX(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0 || sfxSource == null)
            return;

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        PlaySFX(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null)
            return;

        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.clip = clip;
        musicSource.loop = true;
        ApplyMusicVolume();
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }
}