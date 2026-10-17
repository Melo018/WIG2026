
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngredientDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject dragVisual;
    private bool canDrag;
    public bool CanDrag => canDrag;

    public string IngredientId { get; private set; }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        IngredientId = gameObject.name.Replace("ing_", "");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canDrag = false;
        IngredientInventory inventory =
            FindFirstObjectByType<IngredientInventory>();

        if (inventory == null || inventory.GetQuantity(IngredientId) <= 0)
            return;

        canDrag = true;

        dragVisual = new GameObject("Drag_" + IngredientId, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(CanvasGroup));
        dragVisual.transform.SetParent(canvas.transform, false);
        dragVisual.transform.SetAsLastSibling();

        RectTransform visualRect = dragVisual.GetComponent<RectTransform>();
        visualRect.sizeDelta = rectTransform.rect.size;

        Image sourceImage = GetComponent<Image>();
        Image visualImage = dragVisual.GetComponent<Image>();
        visualImage.sprite = sourceImage.sprite;
        visualImage.preserveAspect = true;
        visualImage.raycastTarget = false;

        dragVisual.GetComponent<CanvasGroup>().blocksRaycasts = false;
        canvasGroup.blocksRaycasts = false;

        UpdateDragPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag)
        return;

        UpdateDragPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag)
        return;

        canvasGroup.blocksRaycasts = true;

        if (dragVisual != null)
            Destroy(dragVisual);
        
        //canDrag = false;
    }

    private void UpdateDragPosition(PointerEventData eventData)
    {
        if (dragVisual == null)
            return;

        RectTransform visualRect = dragVisual.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        visualRect.localPosition = localPoint;
    }
}