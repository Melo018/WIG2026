using System.Collections.Generic;
using UnityEngine;

public class IngredientUnlocker : MonoBehaviour
{
    [SerializeField] private IngredientUnlockDisplay[] ingredientsToUnlock;

    private readonly HashSet<string> unlockedIngredients = new();

    public bool UnlockIngredient(string ingredientId)
    {
        if (!unlockedIngredients.Add(ingredientId))
            return false;

        foreach (IngredientUnlockDisplay display in ingredientsToUnlock)
        {
            if (display != null && display.name == "Img_slot_" + ingredientId)
            {
                display.Unlock();
                Debug.Log("Ingrédient débloqué : " + ingredientId);
                return true;
            }
        }

        unlockedIngredients.Remove(ingredientId);
        Debug.LogWarning("Ingrédient à débloquer introuvable : " + ingredientId);
        return false;
    }
}