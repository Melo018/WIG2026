
using UnityEngine;

public static class IngredientSpriteLoader
{
    public static Sprite GetSprite(string ingredientId)
    {
        Sprite sprite = Resources.Load<Sprite>("Art/Ingredients/ing_" + ingredientId);

        if (sprite == null)
            Debug.LogError("Sprite introuvable pour l'ingrédient : " + ingredientId);

        return sprite;
    }
}