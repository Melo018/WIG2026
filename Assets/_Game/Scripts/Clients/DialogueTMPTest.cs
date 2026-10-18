
using UnityEngine;
using TMPro;

public class DialogueTMPTest : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;

    [ContextMenu("Test affichage TMP")]
    public void TestAffichageTMP()
    {
        if (dialogueText == null)
        {
            Debug.LogError("Dialogue Text non assigné.");
            return;
        }

        dialogueText.text = "TEST AFFICHAGE COMPLET";
        dialogueText.maxVisibleCharacters = int.MaxValue;
        dialogueText.ForceMeshUpdate();

        Debug.Log(
            "Caractères : " + dialogueText.textInfo.characterCount +
            " | Visibles : " + dialogueText.maxVisibleCharacters
        );
    }
}