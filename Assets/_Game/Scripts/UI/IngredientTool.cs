
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientTool : MonoBehaviour, IDropHandler
{
    [SerializeField] private bool createsPlus;
    [SerializeField] private IngredientUnlocker unlocker;
    [SerializeField] private IngredientInventory inventory;

    private void Awake()
    {
        if (inventory == null)
            inventory = FindFirstObjectByType<IngredientInventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        IngredientDrag ingredient =
            eventData.pointerDrag.GetComponent<IngredientDrag>();

        if (ingredient == null || !ingredient.CanDrag)
            return;

        string ingredientId = ingredient.IngredientId;

        if (ingredientId != "UA" && ingredientId != "UB")
            return;

        if (inventory == null || inventory.GetQuantity(ingredientId) <= 0)
            return;

        string suffix = createsPlus ? "+" : "-";
        string transformedId = ingredientId + suffix;

        if (!inventory.TryRemoveIngredient(ingredientId))
            return;

        inventory.RecordIngredientUse(ingredientId);
        inventory.AddIngredient(transformedId);

        if (unlocker != null)
            unlocker.UnlockIngredient(transformedId);

        Debug.Log("Transformation : " + ingredientId + " → " + transformedId);
    }
}