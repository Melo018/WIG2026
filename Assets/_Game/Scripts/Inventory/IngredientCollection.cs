
using UnityEngine;

public class IngredientCollection : MonoBehaviour
{
    [SerializeField] private IngredientInventory inventory;

    private readonly string[] ingredientIds = { "BA", "BB", "UA", "UB" };

    private void Awake()
    {
        if (inventory == null)
            inventory = FindFirstObjectByType<IngredientInventory>();
    }

    public void CollectIngredients()
    {
        if (inventory == null)
        {
            Debug.LogWarning("IngredientInventory introuvable.");
            return;
        }

        int amount = Random.Range(2, 6);

        for (int i = 0; i < amount; i++)
        {
            string ingredientId = GetWeightedIngredient();
            inventory.AddIngredient(ingredientId);
        }

        Debug.Log("Collecte terminée : " + amount + " ingrédients récupérés.");
    }

    private string GetWeightedIngredient()
    {
        int totalUses = 0;

        foreach (string id in ingredientIds)
            totalUses += inventory.GetTotalUseCount(id);

        // Tant qu'aucun ingrédient n'a été consommé, tirage uniforme.
        if (totalUses == 0)
            return ingredientIds[Random.Range(0, ingredientIds.Length)];

        int totalWeight = 0;
        int[] weights = new int[ingredientIds.Length];

        for (int i = 0; i < ingredientIds.Length; i++)
        {
            int uses = inventory.GetTotalUseCount(ingredientIds[i]);

            // Chaque ingrédient conserve une chance minimale.
            weights[i] = uses + 1;
            totalWeight += weights[i];
        }

        int roll = Random.Range(0, totalWeight);

        for (int i = 0; i < ingredientIds.Length; i++)
        {
            roll -= weights[i];

            if (roll < 0)
                return ingredientIds[i];
        }

        return ingredientIds[0];
    }
}