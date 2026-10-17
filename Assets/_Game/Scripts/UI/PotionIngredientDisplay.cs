
using UnityEngine;
using UnityEngine.UI;

public class PotionIngredientDisplay : MonoBehaviour
{
    [SerializeField] private Image[] ingredientSlots;

    public void DisplayIngredients(Sprite ingredientA, Sprite ingredientB, Sprite ingredientC)
    {
        Sprite[] ingredients = { ingredientA, ingredientB, ingredientC };

        for (int i = 0; i < ingredientSlots.Length && i < ingredients.Length; i++)
        {
            ingredientSlots[i].sprite = ingredients[i];
            ingredientSlots[i].enabled = ingredients[i] != null;
        }
    }
}