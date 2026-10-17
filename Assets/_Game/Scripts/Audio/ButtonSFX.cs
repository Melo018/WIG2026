using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] clickSounds;

    public void PlayClickSound()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayRandomSFX(clickSounds);
    }
}