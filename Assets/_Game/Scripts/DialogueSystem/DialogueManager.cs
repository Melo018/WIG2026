
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private CharacterStateStore stateStore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Plusieurs DialogueManager sont présents dans la scène.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        stateStore = new CharacterStateStore();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void TriggerAction(
        string characterId,
        string actionId,
        string payloadJson = "{}")
    {
        if (string.IsNullOrWhiteSpace(characterId))
        {
            Debug.LogWarning("DialogueManager : characterId manquant.");
            return;
        }

        if (string.IsNullOrWhiteSpace(actionId))
        {
            Debug.LogWarning("DialogueManager : actionId manquant.");
            return;
        }

        DialogueEvent dialogueEvent = new DialogueEvent(
            characterId,
            actionId,
            payloadJson
        );

        CharacterDialogueState state = stateStore.GetOrCreate(characterId);
        state.RecordEvent(dialogueEvent);

        Debug.Log(
            "DialogueManager : événement reçu — personnage : "
            + characterId
            + " | action : "
            + actionId
        );
    }

    public CharacterDialogueState GetCharacterState(string characterId)
    {
        if (stateStore == null)
            return null;

        return stateStore.TryGet(characterId, out CharacterDialogueState state)
            ? state
            : null;
    }
}