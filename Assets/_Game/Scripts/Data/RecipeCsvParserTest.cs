
using UnityEngine;

public class RecipeCsvParserTest : MonoBehaviour
{
    void Start()
    {
        var recipes = RecipeCsvParser.Parse(CsvLoader.Load("Plants/Plant_A"));
        RecipeValidator.Validate(recipes);
        RecipeValidator.ValidatePlantA(recipes);

        RecipeRegistry registry = new RecipeRegistry(recipes);
        RecipeData original = recipes[0];

        if (!registry.TryGetRecipe(original.Ing1, original.Ing2, original.Ing3, out RecipeData found))
            throw new System.Exception("Recette introuvable");

        if (!registry.TryGetRecipe(original.Ing3, original.Ing1, original.Ing2, out RecipeData reordered))
            throw new System.Exception("Recette introuvable après changement d'ordre");

        if (found.Result != reordered.Result)
            throw new System.Exception("Résultats différents selon l'ordre");

        Debug.Log("Recherche de recette validée : " + found.Result);
    }
}