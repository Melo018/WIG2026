
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClientDialogue : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button continueButton;

    private void Awake()
    {
        if (dialogueText == null)
            dialogueText = GetComponentInChildren<TMP_Text>(true);

        if (continueButton != null)
            continueButton.onClick.AddListener(CloseDialogue);
        else
            Debug.LogError("ClientDialogue : bouton Continuer non assigné.", this);
    }

    private void Start()
    {
        if (dialogueText != null)
            dialogueText.text = "Hi Asmodel, I want a potion";
    }

    private void CloseDialogue()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator PlayLines(DialogueLine[] lines)
    {
        yield break;
    }

    private void OnDestroy()
    {
        if (continueButton != null)
            continueButton.onClick.RemoveListener(CloseDialogue);
    }
}