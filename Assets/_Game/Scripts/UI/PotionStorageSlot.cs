
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionStorageSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image slotImage;

    [SerializeField] private PotionTagDisplay tagDisplay;

    public PotionData StoredPotion { get; private set; }

    void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    public bool StorePotion(PotionData potion)
    {
        if (potion == null || StoredPotion != null)
            return false;

        string result = potion.RecipeFound ? potion.RecipeResult : "null";
        Sprite sprite = PotionSpriteLoader.GetSprite(result);

        if (sprite == null)
            return false;

        StoredPotion = potion;
        slotImage.sprite = sprite;
        slotImage.preserveAspect = true;
        slotImage.enabled = true;

        return true;
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (StoredPotion != null && tagDisplay != null)
            tagDisplay.Display(StoredPotion);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tagDisplay != null)
            tagDisplay.Display(null);
    }

    public void RemovePotion()
    {
        StoredPotion = null;
        slotImage.sprite = null;
        slotImage.enabled = true;

        if (tagDisplay != null)
            tagDisplay.Display(null);
    }
}