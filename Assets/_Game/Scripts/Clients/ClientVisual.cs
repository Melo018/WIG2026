
using UnityEngine;
using UnityEngine.UI;

public class ClientVisual : MonoBehaviour
{
    [SerializeField] private Image characterImage;

    public Image CharacterImage => characterImage;

    public void SetVisible(bool visible)
    {
        if (characterImage != null)
            characterImage.gameObject.SetActive(visible);
    }
}