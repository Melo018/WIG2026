
using UnityEngine;

public class PotionServiceTest : MonoBehaviour
{
    void Start()
    {
        var associations = MatrixLoader.Load(CsvLoader.Load("Data/Matrix"));
        var recipeData = RecipeCsvParser.Parse(CsvLoader.Load("Plants/Plant_A"));
        var registry = new RecipeRegistry(recipeData);
        var service = new PotionService(associations, registry);
        PotionData potion = service.CreatePotion("BA", "UA", "UB");

        Debug.Log("Ingrédients : " + string.Join(", ", potion.Ingredients));
        Debug.Log("Tags : " + string.Join(", ", potion.Tags));
        Debug.Log("Recette trouvée : " + potion.RecipeFound);
        Debug.Log("Résultat : " + (potion.RecipeFound ? potion.RecipeResult : "Recette inconnue"));
    }
}