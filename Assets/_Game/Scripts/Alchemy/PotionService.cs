
using System;
using System.Collections.Generic;

public class PotionService
{
    readonly Dictionary<string, List<string>> associations;
    readonly RecipeRegistry recipes;

    public PotionService(Dictionary<string, List<string>> associations, RecipeRegistry recipes)
    {
        this.associations = associations;
        this.recipes = recipes;
    }

    public PotionData CreatePotion(string a, string b, string c)
    {
        List<string> tags = PotionTagCalculator.Calculate(a, b, c, associations);
        PotionData potion = new PotionData();
        potion.Ingredients.AddRange(new[] { a, b, c });
        potion.Tags = tags;

        if (recipes.TryGetRecipe(a, b, c, out RecipeData recipe))
        {
            potion.RecipeFound = true;
            potion.RecipeResult = recipe.Result;
        }
        return potion;
    }
}