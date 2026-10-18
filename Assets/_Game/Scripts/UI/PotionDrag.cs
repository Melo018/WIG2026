
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private PotionStorageSlot storageSlot;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GameObject dragVisual;

    void Awake()
    {
        storageSlot = GetComponent<PotionStorageSlot>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (storageSlot == null || storageSlot.StoredPotion == null)
            return;

        dragVisual = new GameObject("PotionDragVisual", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        dragVisual.transform.SetParent(canvas.transform, false);
        dragVisual.transform.SetAsLastSibling();

        RectTransform visualRect = dragVisual.GetComponent<RectTransform>();
        visualRect.sizeDelta = GetComponent<RectTransform>().rect.size;

        Image visualImage = dragVisual.GetComponent<Image>();
        visualImage.sprite = GetComponent<Image>().sprite;
        visualImage.preserveAspect = true;
        visualImage.raycastTarget = false;

        canvasGroup.blocksRaycasts = false;
        UpdatePosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdatePosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (dragVisual != null)
            Destroy(dragVisual);
    }

    private void UpdatePosition(PointerEventData eventData)
    {
        if (dragVisual == null)
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        dragVisual.GetComponent<RectTransform>().localPosition = localPoint;
    }
}