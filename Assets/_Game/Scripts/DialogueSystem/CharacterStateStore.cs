
using System.Collections.Generic;

public class CharacterStateStore
{
    private readonly Dictionary<string, CharacterDialogueState> characters =
        new Dictionary<string, CharacterDialogueState>();

    public CharacterDialogueState GetOrCreate(string characterId)
    {
        if (string.IsNullOrWhiteSpace(characterId))
            return null;

        if (characters.TryGetValue(characterId, out CharacterDialogueState state))
            return state;

        state = new CharacterDialogueState(characterId);
        characters.Add(characterId, state);

        return state;
    }

    public bool TryGet(string characterId, out CharacterDialogueState state)
    {
        if (string.IsNullOrWhiteSpace(characterId))
        {
            state = null;
            return false;
        }

        return characters.TryGetValue(characterId, out state);
    }

    public bool Contains(string characterId)
    {
        return !string.IsNullOrWhiteSpace(characterId)
            && characters.ContainsKey(characterId);
    }

    public int Count => characters.Count;

    public void Clear()
    {
        characters.Clear();
    }
}