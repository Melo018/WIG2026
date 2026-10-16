
using System.Collections.Generic;
using UnityEngine;

public static class TagSpriteLoader
{
    static Dictionary<string, Sprite> sprites;

    public static void LoadAll()
    {
        sprites = new Dictionary<string, Sprite>();
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>("Art/Tags");

        foreach (Sprite sprite in loadedSprites)
        {
            string tag = sprite.name.StartsWith("tag_") ? sprite.name.Substring(4) : sprite.name;

            if (sprites.ContainsKey(tag))
                Debug.LogWarning("Sprite de tag en double : " + tag);
            else
                sprites.Add(tag, sprite);
        }

        foreach (string tag in GetKnownTags())
            if (!sprites.ContainsKey(tag))
                Debug.LogWarning("Sprite manquant pour le tag : " + tag);
    }

    public static Sprite GetSprite(string tag)
    {
        if (sprites == null) LoadAll();
        sprites.TryGetValue(tag, out Sprite sprite);
        return sprite;
    }

    static string[] GetKnownTags()
    {
        return new[]
        {
            "amour", "aliment", "transformation", "mouvement", "emotion",
            "corps", "protection", "froid", "chaud", "soleil", "rejet",
            "peur", "symptome", "morose", "nouveaute", "degout"
        };
    }
}