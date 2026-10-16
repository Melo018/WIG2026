
using UnityEngine;

public static class CsvLoader
{
    public static string Load(string path)
    {
        TextAsset file = Resources.Load<TextAsset>(path);
        if (file == null) throw new System.Exception("CSV introuvable : " + path);
        return file.text;
    }
}