
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    [TextArea(2, 4)]
    public string text;
}

public class ClientDialogue : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private AudioSource audioSource;

    [Header("Typing")]
    [SerializeField, Min(0.001f)] private float characterDelay = 0.035f;
    [SerializeField, Min(0.01f)] private float voiceSoundInterval = 0.15f;
    [SerializeField] private AudioClip[] blablaSounds;

    private bool isTyping;
    private bool skipTyping;
    private bool advanceRequested;

    private void Awake()
    {
        if (dialogueText == null)
            dialogueText = GetComponentInChildren<TMP_Text>(true);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clic reçu par ClientDialogue sur : " + gameObject.name);

        if (isTyping)
            skipTyping = true;
        else
            advanceRequested = true;
    }

    public IEnumerator PlayLines(DialogueLine[] lines)
    {
        if (dialogueText == null || lines == null || lines.Length == 0)
            yield break;

        foreach (DialogueLine line in lines)
        {
            if (line == null || string.IsNullOrWhiteSpace(line.text))
                continue;

            advanceRequested = false;
            yield return StartCoroutine(TypeLine(line.text));

            while (!advanceRequested)
                yield return null;
        }
    }

    private IEnumerator TypeLine(string line)
    {
        Debug.Log("A — Début TypeLine");

        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        dialogueText.ForceMeshUpdate();

        isTyping = true;
        skipTyping = false;

        int totalCharacters = dialogueText.textInfo.characterCount;
        Debug.Log("B — Nombre de caractères : " + totalCharacters);

        for (int i = 0; i < totalCharacters; i++)
        {
            dialogueText.maxVisibleCharacters = i + 1;
            Debug.Log("C — Lettre " + (i + 1));

            yield return null;
        }

        dialogueText.maxVisibleCharacters = int.MaxValue;
        isTyping = false;

        Debug.Log("D — Fin TypeLine");
    }

    private void OnDisable()
    {
        Debug.LogWarning(
            "ClientDialogue désactivé : " + gameObject.name
        );
    }

    private void OnEnable()
    {
        Debug.Log("ClientDialogue activé : " + gameObject.name);
    }


    private void PlayRandomBlabla()
    {
        if (audioSource == null || blablaSounds == null || blablaSounds.Length == 0)
            return;

        AudioClip clip = blablaSounds[Random.Range(0, blablaSounds.Length)];

        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}