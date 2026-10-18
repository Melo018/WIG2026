
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotionClient : MonoBehaviour, IDropHandler
{
    [Header("Dialogues")]
    [SerializeField] private DialogueLine[] firstVisitLines;
    [SerializeField] private DialogueLine[] returnVisitLines;
    [SerializeField] private DialogueLine[] goodReactionLines;
    [SerializeField] private DialogueLine[] badReactionLines;
    [SerializeField] private DialogueLine[] savedLines;
    [SerializeField] private DialogueLine[] lostLines;

    [Header("Audio")]
    [SerializeField] private AudioClip[] goodSounds;
    [SerializeField] private AudioClip[] badSounds;

    [Header("References")]
    [SerializeField] private ClientMorality clientMorality;
    [SerializeField] private ClientRewardService rewardService;
    [SerializeField] private ClientGameManager gameManager;
    [SerializeField] private ClientDialogue clientDialogue;

    private bool isReacting;
    private bool hasVisitedBefore;

    private void Awake()
    {
        if (clientMorality == null)
            clientMorality = GetComponent<ClientMorality>();

        if (rewardService == null)
            rewardService = FindFirstObjectByType<ClientRewardService>();

        if (gameManager == null)
            gameManager = FindFirstObjectByType<ClientGameManager>();

        if (clientDialogue == null)
            clientDialogue = GetComponentInChildren<ClientDialogue>(true);
    }

    private void OnEnable()
    {
        if (isReacting || clientDialogue == null)
            return;

        StartCoroutine(ShowVisitDialogue());
    }

    private IEnumerator ShowVisitDialogue()
    {
        isReacting = true;

        DialogueLine[] lines = hasVisitedBefore
            ? returnVisitLines
            : firstVisitLines;

        yield return StartCoroutine(clientDialogue.PlayLines(lines));

        isReacting = false;
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

        DialogueLine[] reactionLines = goodResult
            ? goodReactionLines
            : badReactionLines;

        if (clientDialogue != null)
            yield return StartCoroutine(clientDialogue.PlayLines(reactionLines));

        hasVisitedBefore = true;

        if (thresholdReached)
        {
            bool saved = clientMorality.Morality == 3;

            DialogueLine[] endingLines = saved ? savedLines : lostLines;

            if (clientDialogue != null)
                yield return StartCoroutine(clientDialogue.PlayLines(endingLines));

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