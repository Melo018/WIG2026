
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerEndScreens : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("StartMenu");
    }
    
}
