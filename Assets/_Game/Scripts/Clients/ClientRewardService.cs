
using UnityEngine;

public class ClientRewardService : MonoBehaviour
{
    [SerializeField] private IngredientInventory inventory;

    private void Awake()
    {
        if (inventory == null)
            inventory = FindFirstObjectByType<IngredientInventory>();
    }

    public void ApplyReward(string result)
    {
        if (inventory == null)
        {
            Debug.LogWarning("IngredientInventory introuvable.");
            return;
        }

        switch (result?.ToLowerInvariant())
        {
            case "parfait":
                AddRandom("UA", "UB", 1, 2);
                AddRandom("BA", null, 1, 3);
                AddRandom("BB", null, 1, 3);
                break;

            case "bon":
                AddRandom("UA", "BA", 1, 4);
                break;

            case "change":
                RemoveRandom("BA", "BB", 1, 2);
                AddRandom("UB", "BB", 1, 1);
                break;

            case "null":
                RemoveRandom("UA", "UB", "BB", "BA");
                break;

            default:
                Debug.LogWarning("Résultat inconnu : " + result);
                return;
        }

        Debug.Log("Récompense appliquée : " + result);
    }

    private void AddRandom(string first, string second, int min, int max)
    {
        string ingredient = second == null
            ? first
            : Random.value < 0.5f ? first : second;

        inventory.AddIngredient(ingredient, Random.Range(min, max + 1));
    }

    private void RemoveRandom(string first, string second, int min, int max)
    {
        string ingredient = Random.value < 0.5f ? first : second;
        int amount = Random.Range(min, max + 1);
        int available = inventory.GetQuantity(ingredient);

        inventory.TryRemoveIngredient(ingredient, Mathf.Min(amount, available));
    }

    private void RemoveRandom(string first, string second, string third, string fourth)
    {
        string[] ingredients = { first, second, third, fourth };
        string ingredient = ingredients[Random.Range(0, ingredients.Length)];
        int amount = Random.Range(1, 5);
        int available = inventory.GetQuantity(ingredient);

        inventory.TryRemoveIngredient(ingredient, Mathf.Min(amount, available));
    }
}