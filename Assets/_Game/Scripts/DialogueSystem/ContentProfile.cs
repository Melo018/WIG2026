
using System;

[Serializable]
public class ContentProfile
{
    public string profileId;
    public string versionMode;
    public string[] allowedCharacterIds;
    public string[] allowedDialogueIds;
    public bool endDemoAfterFirstPotionReaction;
    public string demoEndingDialogueId;

    public bool IsDemo
    {
        get { return versionMode == "demo"; }
    }

    public bool AllowsCharacter(string characterId)
    {
        if (allowedCharacterIds == null)
            return false;

        foreach (string id in allowedCharacterIds)
        {
            if (id == "*" || id == characterId)
                return true;
        }

        return false;
    }

    public bool AllowsDialogue(string dialogueId)
    {
        if (allowedDialogueIds == null)
            return false;

        foreach (string id in allowedDialogueIds)
        {
            if (id == "*" || id == dialogueId)
                return true;
        }

        return false;
    }
}