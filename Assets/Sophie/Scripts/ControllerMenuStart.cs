using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMenuStart : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
}
