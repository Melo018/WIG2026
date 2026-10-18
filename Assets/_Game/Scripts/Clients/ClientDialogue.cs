
using TMPro;
using UnityEngine;

public class ClientDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBackground;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private string[] phrases;

    private int currentIndex;

    public void ShowNextPhrase()
    {
        if (phrases == null || phrases.Length == 0 || dialogueText == null)
        {
            HideDialogue();
            return;
        }

        if (currentIndex >= phrases.Length)
        {
            HideDialogue();
            return;
        }

        dialogueText.text = phrases[currentIndex];
        currentIndex++;
    }

    public void ResetDialogue()
    {
        currentIndex = 0;

        if (dialogueText != null)
            dialogueText.text = "";
    }

    private void HideDialogue()
    {
        if (dialogueBackground != null)
            dialogueBackground.SetActive(false);
    }
}