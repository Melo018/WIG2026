
using System;
using System.Collections.Generic;
using System.Linq;

public static class PotionTagCalculator
{
    public static List<string> Calculate(string a, string b, string c, Dictionary<string, List<string>> associations)
    {
        if (a == b || a == c || b == c)
            throw new Exception("Une potion doit contenir trois ingrédients différents.");

        string[][] pairs = { new[] { a, b }, new[] { a, c }, new[] { b, c } };
        HashSet<string> tags = new HashSet<string>();

        foreach (string[] pair in pairs)
        {
            string key = MatrixLoader.GetKey(pair[0], pair[1]);
            if (!associations.TryGetValue(key, out List<string> pairTags))
                throw new Exception("Association manquante : " + pair[0] + " / " + pair[1]);

            foreach (string tag in pairTags)
                tags.Add(tag);
        }

        return tags.OrderBy(tag => tag, StringComparer.Ordinal).ToList();
    }
}