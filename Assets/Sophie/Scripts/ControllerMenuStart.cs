using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControllerMenuStart : MonoBehaviour
{
    public AudioSource clickSound;

    public void OnStartClick()
    {
        StartCoroutine(PlayThenLoad());
    }

    private IEnumerator PlayThenLoad()
    {
        clickSound.Play();
        yield return new WaitForSecondsRealtime(clickSound.clip.length);
        SceneManager.LoadScene("SampleScene");
    }
}