
using System;
using System.Collections.Generic;

public class RecipeRegistry
{
    readonly Dictionary<string, RecipeData> recipes = new Dictionary<string, RecipeData>();

    string GetKey(string a, string b, string c)
    {
        string[] ingredients = { a, b, c };
        Array.Sort(ingredients, StringComparer.Ordinal);
        return string.Join("|", ingredients);
    }

    public RecipeRegistry(List<RecipeData> data)
    {
        foreach (RecipeData recipe in data)
            recipes.Add(GetKey(recipe.Ing1, recipe.Ing2, recipe.Ing3), recipe);
    }

    public bool TryGetRecipe(string a, string b, string c, out RecipeData recipe)
    {
        return recipes.TryGetValue(GetKey(a, b, c), out recipe);
    }
}