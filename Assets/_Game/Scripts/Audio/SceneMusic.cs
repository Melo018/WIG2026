
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;

    private void Start()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(backgroundMusic);
    }
}