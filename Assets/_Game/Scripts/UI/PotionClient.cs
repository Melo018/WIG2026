using UnityEngine;
using UnityEngine.EventSystems;

public class PotionClient : MonoBehaviour, IDropHandler
{
    [SerializeField] private AudioClip[] potionOnCharacterSounds;

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

        PotionData potion = slot.StoredPotion;

        ReceivePotion(potion);
        slot.RemovePotion();

        if (AudioManager.Instance != null)
        AudioManager.Instance.PlayRandomSFX(potionOnCharacterSounds);
    }

    private void ReceivePotion(PotionData potion)
    {
        //Debug.Log("Le client reçoit une potion avec les tags : " + string.Join(", ", potion.Tags));
    }
}