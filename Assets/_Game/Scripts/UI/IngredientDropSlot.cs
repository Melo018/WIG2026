
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngredientDropSlot : MonoBehaviour, IDropHandler
{
    private Image slotImage;
    public string SelectedIngredientId { get; private set; }
    public System.Action OnIngredientChanged;
    [SerializeField] private AudioClip dropSound;

    void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        IngredientDrag ingredient = eventData.pointerDrag.GetComponent<IngredientDrag>();

        if (!ingredient.CanDrag)
            return;

        if (ingredient == null)
            return;

        Image ingredientImage = ingredient.GetComponent<Image>();

        if (ingredientImage == null || ingredientImage.sprite == null)
            return;

        IngredientDropSlot[] slots = FindObjectsByType<IngredientDropSlot>(FindObjectsSortMode.None);

        foreach (IngredientDropSlot slot in slots)
        {
            if (slot != this && slot.SelectedIngredientId == ingredient.IngredientId)
            {
                Debug.Log("Cet ingrédient est déjà dans le chaudron.");
                return;
            }
        }

        SelectedIngredientId = ingredient.IngredientId;
        slotImage.sprite = ingredientImage.sprite;
        slotImage.preserveAspect = true;
        slotImage.enabled = true;

        OnIngredientChanged?.Invoke();
        if (AudioManager.Instance != null)
        AudioManager.Instance.PlaySFX(dropSound);

        Debug.Log("Slot " + name + " : " + SelectedIngredientId);
    }

    public void Clear()
    {
        SelectedIngredientId = null;
        slotImage.sprite = null;
        slotImage.enabled = true;
        OnIngredientChanged?.Invoke();
    }
}