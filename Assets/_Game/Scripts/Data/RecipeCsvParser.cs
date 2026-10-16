
using System.Collections.Generic;
using UnityEngine;

public static class RecipeCsvParser
{
    public static List<RecipeData> Parse(string csv)
    {
        List<RecipeData> recipes = new List<RecipeData>();
        string[] lines = csv.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] columns = line.Split(';');
            if (columns.Length != 4)
                throw new System.Exception("CSV recettes : 4 colonnes attendues, ligne " + (i + 1));

            recipes.Add(new RecipeData(
                columns[0].Trim(),
                columns[1].Trim(),
                columns[2].Trim(),
                columns[3].Trim()
            ));
        }
        return recipes;
    }
}