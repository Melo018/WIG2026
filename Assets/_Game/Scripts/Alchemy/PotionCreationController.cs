
using UnityEngine;
using UnityEngine.UI;

public class PotionCreationController : MonoBehaviour
{
    [SerializeField] private IngredientDropSlot[] ingredientSlots;
    [SerializeField] private PotionTagDisplay tagDisplay;
    [SerializeField] private Image validationButtonImage;
    [SerializeField] private Outline validationButtonOutline;
    [SerializeField] private Color activeOutlineColor = Color.green;
    [SerializeField] private string clientId = "A";
    [SerializeField] private IngredientInventory inventory;

    private AlchemyService alchemy;

    void Awake()
    {
        alchemy = new AlchemyService(clientId);

        if (inventory == null)
            inventory = FindFirstObjectByType<IngredientInventory>();

        alchemy = new AlchemyService(clientId);
    }

    
    public void SetClient(string newClientId)
    {
        if (string.IsNullOrWhiteSpace(newClientId))
        {
            Debug.LogWarning("Identifiant client vide.");
            return;
        }

        try
        {
            AlchemyService newAlchemy = new AlchemyService(newClientId);
            clientId = newClientId;
            alchemy = newAlchemy;

            if (ingredientSlots != null)
            {
                foreach (IngredientDropSlot slot in ingredientSlots)
                {
                    if (slot != null)
                        slot.Clear();
                }
            }

            if (tagDisplay != null)
                tagDisplay.Display(null);

            UpdateValidationButton();

            Debug.Log("CSV chargé pour le client : " + newClientId);
        }
        catch (System.Exception e)
        {
            Debug.LogError(
                "Impossible de charger le CSV du client " +
                newClientId + " : " + e.Message
            );
        }
    }


    void OnEnable()
    {
        if (ingredientSlots == null)
            return;

        foreach (IngredientDropSlot slot in ingredientSlots)
        {
            if (slot != null)
            {
                slot.OnIngredientChanged += UpdateValidationButton;
                slot.OnIngredientChanged += UpdatePotionTags;
            }
        }

        UpdateValidationButton();
    }

    void OnDisable()
    {
        if (ingredientSlots == null)
            return;

        foreach (IngredientDropSlot slot in ingredientSlots)
        {
            if (slot != null)
            {
                slot.OnIngredientChanged -= UpdateValidationButton;
                slot.OnIngredientChanged -= UpdatePotionTags;
            }
        }
    }

    void UpdateValidationButton()
    {
        bool allFilled = ingredientSlots != null && ingredientSlots.Length == 3;

        if (allFilled)
        {
            foreach (IngredientDropSlot slot in ingredientSlots)
            {
                if (slot == null || string.IsNullOrEmpty(slot.SelectedIngredientId))
                {
                    allFilled = false;
                    break;
                }
            }
        }

        if (validationButtonImage != null)
            validationButtonImage.color = Color.white;

        if (validationButtonOutline != null)
        {
            validationButtonOutline.enabled = allFilled;

            if (allFilled)
                validationButtonOutline.effectColor = activeOutlineColor;
        }
    }

    void UpdatePotionTags()
    {
        if (ingredientSlots == null || ingredientSlots.Length != 3)
            return;

        foreach (IngredientDropSlot slot in ingredientSlots)
        {
            if (slot == null || string.IsNullOrEmpty(slot.SelectedIngredientId))
            {
                if (tagDisplay != null)
                    tagDisplay.Display(null);

                return;
            }
        }

        PotionData potion = alchemy.CreatePotion(
            ingredientSlots[0].SelectedIngredientId,
            ingredientSlots[1].SelectedIngredientId,
            ingredientSlots[2].SelectedIngredientId
        );

        if (tagDisplay != null)
            tagDisplay.Display(potion);
    }

    public void CreatePotion()
    {
        if (inventory == null || ingredientSlots == null || ingredientSlots.Length != 3)
            return;

        foreach (IngredientDropSlot slot in ingredientSlots)
        {
            if (slot == null || string.IsNullOrEmpty(slot.SelectedIngredientId))
            {
                Debug.LogWarning("Les trois ingrédients doivent être sélectionnés.");
                return;
            }

            if (inventory.GetQuantity(slot.SelectedIngredientId) < 1)
            {
                Debug.LogWarning("Stock insuffisant : " + slot.SelectedIngredientId);
                return;
            }
        }

        PotionStorageSlot[] storageSlots =
            FindObjectsByType<PotionStorageSlot>(FindObjectsSortMode.None);

        PotionStorageSlot freeSlot = null;

        foreach (PotionStorageSlot storageSlot in storageSlots)
        {
            if (storageSlot != null && storageSlot.StoredPotion == null)
            {
                freeSlot = storageSlot;
                break;
            }
        }

        if (freeSlot == null)
        {
            Debug.LogWarning("Les quatre emplacements de stockage sont pleins.");
            return;
        }

        PotionData potion = alchemy.CreatePotion(
            ingredientSlots[0].SelectedIngredientId,
            ingredientSlots[1].SelectedIngredientId,
            ingredientSlots[2].SelectedIngredientId
        );

        if (!freeSlot.StorePotion(potion))
            return;

        foreach (IngredientDropSlot slot in ingredientSlots)
        {
            string ingredientId = slot.SelectedIngredientId;

            if (!inventory.TryRemoveIngredient(ingredientId))
            {
                Debug.LogError("Erreur inattendue lors de la consommation des ingrédients.");
                return;
            }

            inventory.RecordPotionUse(ingredientId);
            inventory.RecordIngredientUse(ingredientId);
        }

        foreach (IngredientDropSlot slot in ingredientSlots)
            slot.Clear();

        if (tagDisplay != null)
            tagDisplay.Display(null);

        Debug.Log("Potion créée : " + (potion.RecipeFound ? potion.RecipeResult : "null"));
    }
}