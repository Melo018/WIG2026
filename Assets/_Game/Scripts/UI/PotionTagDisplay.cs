
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionTagDisplay : MonoBehaviour
{
    [SerializeField] Transform tagContainer;
    [SerializeField] Image tagPrefab;

    public void Display(PotionData potion)
    {
        foreach (Transform child in tagContainer)
            Destroy(child.gameObject);

        if (potion == null)
            return;

        foreach (string tag in potion.Tags)
        {
            Sprite sprite = TagSpriteLoader.GetSprite(tag);
            if (sprite == null) continue;

            Image image = Instantiate(tagPrefab, tagContainer);
            image.sprite = sprite;
            image.preserveAspect = true;
            image.gameObject.SetActive(true);
        }
    }
}