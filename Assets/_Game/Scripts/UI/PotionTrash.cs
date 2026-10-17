
using UnityEngine;
using UnityEngine.EventSystems;

public class PotionTrash : MonoBehaviour, IDropHandler
{
    [SerializeField] private AudioClip[] potionOnTrashSounds;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        PotionDrag potionDrag = eventData.pointerDrag.GetComponent<PotionDrag>();

        if (potionDrag == null)
            return;

        PotionStorageSlot slot = potionDrag.GetComponent<PotionStorageSlot>();

        if (slot == null || slot.StoredPotion == null)
            return;

        slot.RemovePotion();
        Debug.Log("Potion supprimée.");

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayRandomSFX(potionOnTrashSounds);
    }
}