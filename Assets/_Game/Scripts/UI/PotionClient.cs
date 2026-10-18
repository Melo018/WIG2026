
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PotionClient : MonoBehaviour, IDropHandler
{
    [SerializeField] private AudioClip[] goodSounds;
    [SerializeField] private AudioClip[] badSounds;
    [SerializeField] private ClientMorality clientMorality;
    [SerializeField] private ClientRewardService rewardService;
    [SerializeField] private ClientGameManager gameManager;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float reactionDuration = 2f;

    private bool isReacting;

    private void Awake()
    {
        if (clientMorality == null)
            clientMorality = GetComponent<ClientMorality>();

        if (rewardService == null)
            rewardService = FindFirstObjectByType<ClientRewardService>();

        if (gameManager == null)
            gameManager = FindFirstObjectByType<ClientGameManager>();

        if (dialogueText == null)
            dialogueText = GetComponentInChildren<TMP_Text>(true);
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

        if (dialogueText != null)
            dialogueText.text = goodResult ? "Merci !" : "Je ne reviendrai pas !";

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayRandomSFX(
                goodResult ? goodSounds : badSounds
            );
        }

        Debug.Log(
            gameObject.name + " reçoit " + result +
            ". Moralité : " + clientMorality.Morality
        );

        yield return new WaitForSeconds(reactionDuration);

        if (thresholdReached)
        {
            bool saved = clientMorality.Morality == 3;

            if (dialogueText != null)
            {
                dialogueText.text = saved
                    ? "Tu m'as sauvé ! Merci !"
                    : "Je suis perdu à jamais...";
            }

            yield return new WaitForSeconds(reactionDuration);

            if (gameManager != null)
                gameManager.ResolveCurrentClient(saved);
            else
                gameObject.SetActive(false);
        }
        else
        {
            if (gameManager != null)
                gameManager.ReturnCurrentClientToQueue();
        }

        isReacting = false;
    }
}