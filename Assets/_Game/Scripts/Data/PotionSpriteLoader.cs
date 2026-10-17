using UnityEngine;

public static class PotionSpriteLoader
{
    public static Sprite GetSprite(string result)
    {
        string spriteName = result switch
        {
            "parfait" => "pot_parfait",
            "bon" => "pot_bon",
            "change" => "pot_change",
            "null" => "pot_null",
            _ => null
        };

        if (spriteName == null)
            return null;

        Sprite sprite = Resources.Load<Sprite>("Art/Potions/" + spriteName);

        if (sprite == null)
            Debug.LogError("Sprite de potion introuvable : " + spriteName);

        return sprite;
    }
}