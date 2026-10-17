
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInventory : MonoBehaviour
{
    private readonly Dictionary<string, int> quantities = new()
    {
        { "BA", 3 }, { "BB", 3 }, { "UA", 3 }, { "UB", 3 },
        { "UA-", 0 }, { "UA+", 0 }, { "UB-", 0 }, { "UB+", 0 }
    };

    private readonly Dictionary<string, TextMeshProUGUI> counters = new();
    private readonly Dictionary<string, Image> images = new();

    private readonly Dictionary<string, int> potionUseCounts = new()
    {
        { "BA", 0 },
        { "BB", 0 },
        { "UA", 0 },
        { "UB", 0 }
    };

    
    private readonly Dictionary<string, int> totalUseCounts = new()
    {
        { "BA", 0 },
        { "BB", 0 },
        { "UA", 0 },
        { "UB", 0 }
    };


    public void RecordPotionUse(string ingredientId)
    {
        if (potionUseCounts.ContainsKey(ingredientId))
            potionUseCounts[ingredientId]++;
    }

    public int GetPotionUseCount(string ingredientId)
    {
        return potionUseCounts.TryGetValue(ingredientId, out int count)
            ? count
            : 0;
    }

    
    public void RecordIngredientUse(string ingredientId)
    {
        if (totalUseCounts.ContainsKey(ingredientId))
            totalUseCounts[ingredientId]++;
    }

    public int GetTotalUseCount(string ingredientId)
    {
        return totalUseCounts.TryGetValue(ingredientId, out int count)
            ? count
            : 0;
    }


    private void Awake()
    {
        IngredientDrag[] ingredients =
            FindObjectsByType<IngredientDrag>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (IngredientDrag ingredient in ingredients)
        {
            string id = ingredient.gameObject.name.Replace("ing_", "");

            if (string.IsNullOrEmpty(id))
            {
                Debug.LogWarning("IngredientId vide sur : " + ingredient.name);
                continue;
            }

            Transform counter = ingredient.transform.Find("TMP");
            if (counter != null)
            {
                TextMeshProUGUI text = counter.GetComponent<TextMeshProUGUI>();
                if (text != null)
                    counters[id] = text;
            }

            Image image = ingredient.GetComponent<Image>();
            if (image != null)
                images[id] = image;
        }
    }

    private void Start()
    {
        foreach (string id in quantities.Keys)
            UpdateDisplay(id);
    }

    public int GetQuantity(string ingredientId)
    {
        return quantities.TryGetValue(ingredientId, out int quantity)
            ? quantity
            : 0;
    }

    public void AddIngredient(string ingredientId, int amount = 1)
    {
        if (amount <= 0 || !quantities.ContainsKey(ingredientId))
            return;

        quantities[ingredientId] += amount;
        UpdateDisplay(ingredientId);
    }

    public bool TryRemoveIngredient(string ingredientId, int amount = 1)
    {
        if (amount <= 0 || GetQuantity(ingredientId) < amount)
            return false;

        quantities[ingredientId] -= amount;
        UpdateDisplay(ingredientId);
        return true;
    }

    private void UpdateDisplay(string ingredientId)
    {
        int quantity = GetQuantity(ingredientId);

        if (counters.TryGetValue(ingredientId, out TextMeshProUGUI text))
            text.text = quantity.ToString();

        if (images.TryGetValue(ingredientId, out Image image))
            image.color = quantity > 0 ? Color.white : Color.gray;
    }
}