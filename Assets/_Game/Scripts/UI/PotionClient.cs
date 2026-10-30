
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotionClient : MonoBehaviour, IDropHandler
{
    [Header("Audio")]
    [SerializeField] private AudioClip[] goodSounds;
    [SerializeField] private AudioClip[] badSounds;

    [Header("References")]
    [SerializeField] private ClientMorality clientMorality;
    [SerializeField] private ClientRewardService rewardService;
    [SerializeField] private ClientGameManager gameManager;

    private bool isReacting;

    private void Awake()
    {
        if (clientMorality == null)
            clientMorality = GetComponent<ClientMorality>();

        if (rewardService == null)
            rewardService = FindFirstObjectByType<ClientRewardService>();

        if (gameManager == null)
            gameManager = FindFirstObjectByType<ClientGameManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isReacting || eventData.pointerDrag == null)
            return;

        PotionDrag potionDrag = eventData.pointerDrag.GetComponent<PotionDrag>();

        if (potionDrag == null)
            return;

        PotionStorageSlot slot = potionDrag.GetComponent<PotionStorageSlot>();

        if (slot == null || slot.StoredPotion == null)
            return;

        PotionData potion = slot.StoredPotion;
        slot.RemovePotion();

        StartCoroutine(ProcessPotion(potion));
    }

    private IEnumerator ProcessPotion(PotionData potion)
    {
        isReacting = true;

        if (clientMorality == null)
        {
            Debug.LogWarning("ClientMorality manquant sur " + gameObject.name);
            isReacting = false;
            yield break;
        }

        string result = potion.RecipeFound ? potion.RecipeResult : "null";
        bool goodResult = result == "parfait" || result == "bon";

        if (rewardService != null)
            rewardService.ApplyReward(result);

        bool thresholdReached = clientMorality.ApplyPotionResult(result);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayRandomSFX(
                goodResult ? goodSounds : badSounds
            );
        }

        Debug.Log("Potion reçue : " + result);

        if (thresholdReached)
        {
            bool saved = clientMorality.Morality == 3;

            if (gameManager != null)
                gameManager.ResolveCurrentClient(saved);
            else
                gameObject.SetActive(false);
        }
        else if (gameManager != null)
        {
            gameManager.ReturnCurrentClientToQueue();
        }

        isReacting = false;
        yield return null;
    }

    public void ShowMeetingDialogue()
    {
        Debug.Log("Dialogue de rencontre temporairement désactivé pour " + gameObject.name);
    }
}