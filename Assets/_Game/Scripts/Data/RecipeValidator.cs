
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public static class RecipeValidator
{
    static readonly string[] ValidResults = { "parfait", "bon", "change", "null" };

    public static void Validate(List<RecipeData> recipes)
    {
        HashSet<string> knownIngredients = new HashSet<string>(IngredientData.Ingredients);
        HashSet<string> combinations = new HashSet<string>();

        for (int i = 0; i < recipes.Count; i++)
        {
            RecipeData recipe = recipes[i];
            string[] ingredients = { recipe.Ing1, recipe.Ing2, recipe.Ing3 };

            if (!ValidResults.Contains(recipe.Result))
                throw new Exception("Résultat invalide ligne " + (i + 2) + " : " + recipe.Result);

            foreach (string ingredient in ingredients)
                if (!knownIngredients.Contains(ingredient))
                    throw new Exception("Ingrédient inconnu ligne " + (i + 2) + " : " + ingredient);

            if (ingredients.Distinct().Count() != 3)
                throw new Exception("Ingrédients répétés ligne " + (i + 2));

            Array.Sort(ingredients);
            string key = string.Join("|", ingredients);

            if (!combinations.Add(key))
                throw new Exception("Recette en double ligne " + (i + 2) + " : " + key);
        }
    }
    
    public static void ValidatePlantA(List<RecipeData> recipes)
    {
        int parfait = recipes.FindAll(r => r.Result == "parfait").Count;
        int bon = recipes.FindAll(r => r.Result == "bon").Count;
        int change = recipes.FindAll(r => r.Result == "change").Count;
        int nul = recipes.FindAll(r => r.Result == "null").Count;

        if (recipes.Count != 56 || parfait != 2 || bon != 12 || change != 24 || nul != 18)
            throw new Exception("Comptage invalide : total=" + recipes.Count + ", parfait=" + parfait + ", bon=" + bon + ", change=" + change + ", null=" + nul);

        Debug.Log("Client A validé : 56 recettes, 2 parfait, 12 bon, 24 change, 18 null.");
    }
}