
using UnityEngine;

public class RecipeCsvParserTest : MonoBehaviour
{
    
    void Start()
    {
        string[] plants = { "Plant_A", "Plant_B", "Plant_C", "Plant_D" };

        foreach (string plant in plants)
        {
            var recipes = RecipeCsvParser.Parse(CsvLoader.Load("Plants/" + plant));
            RecipeValidator.Validate(recipes);
            Debug.Log(plant + " : " + recipes.Count + " recettes validées.");
        }

        RecipeValidator.ValidatePlantA(RecipeCsvParser.Parse(CsvLoader.Load("Plants/Plant_A")));
    }
}