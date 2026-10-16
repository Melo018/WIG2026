
using UnityEngine;

public class TagSpriteLoaderTest : MonoBehaviour
{
    void Start()
    {
        TagSpriteLoader.LoadAll();
        Debug.Log("Sprite chaud : " + (TagSpriteLoader.GetSprite("chaud") != null));
        Debug.Log("Sprite emotion : " + (TagSpriteLoader.GetSprite("emotion") != null));
    }
}