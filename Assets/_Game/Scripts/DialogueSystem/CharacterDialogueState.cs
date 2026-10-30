
using System;
using System.Collections.Generic;

[Serializable]
public class CharacterDialogueState
{
    public string characterId;
    public Dictionary<string, string> values = new Dictionary<string, string>();
    public List<DialogueEvent> history = new List<DialogueEvent>();
    public HashSet<string> playedDialogueIds = new HashSet<string>();

    public CharacterDialogueState(string characterId)
    {
        this.characterId = characterId;
    }

    public void SetValue(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
            return;

        values[key] = value;
    }

    public bool TryGetValue(string key, out string value)
    {
        return values.TryGetValue(key, out value);
    }

    public void RecordEvent(DialogueEvent dialogueEvent)
    {
        if (dialogueEvent == null)
            return;

        history.Add(dialogueEvent);
    }

    public bool HasPlayedDialogue(string dialogueId)
    {
        return playedDialogueIds.Contains(dialogueId);
    }

    public void MarkDialogueAsPlayed(string dialogueId)
    {
        if (!string.IsNullOrWhiteSpace(dialogueId))
            playedDialogueIds.Add(dialogueId);
    }
}