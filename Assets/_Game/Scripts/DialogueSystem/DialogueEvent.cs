
using System;
using UnityEngine;

[Serializable]
public class DialogueEvent
{
    public string eventId;
    public string characterId;
    public string actionId;
    public string payloadJson;

    public DialogueEvent(
        string characterId,
        string actionId,
        string payloadJson = "{}")
    {
        eventId = Guid.NewGuid().ToString();
        this.characterId = characterId;
        this.actionId = actionId;
        this.payloadJson = string.IsNullOrWhiteSpace(payloadJson)
            ? "{}"
            : payloadJson;
    }
}