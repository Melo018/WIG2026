
using UnityEngine;

public class DialogueSystemTest : MonoBehaviour
{
    private void Start()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("TEST ÉCHOUÉ : DialogueManager introuvable.");
            return;
        }

        DialogueManager.Instance.TriggerAction(
            "test_client_01",
            "client_arrived"
        );

        CharacterDialogueState state =
            DialogueManager.Instance.GetCharacterState("test_client_01");

        if (state == null || state.history.Count != 1)
        {
            Debug.LogError("TEST ÉCHOUÉ : événement non enregistré.");
            return;
        }

        Debug.Log(
            "TEST RÉUSSI : fiche créée, événement enregistré. "
            + "Historique : " + state.history.Count
        );
    }
}