using UnityEngine;

public class IngredientUnlockDisplay : MonoBehaviour
{
    [SerializeField] private GameObject ingredient;
    [SerializeField] private GameObject tagImage;

    void Awake()
    {
        if (tagImage != null)
            tagImage.SetActive(false);
    }

    public void Unlock()
    {
        if (ingredient != null)
            ingredient.SetActive(true);

        if (tagImage != null)
            tagImage.SetActive(true);
    }
}