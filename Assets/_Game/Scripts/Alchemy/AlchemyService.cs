
using System.Collections.Generic;

public class AlchemyService
{
    readonly PotionService potionService;

    public AlchemyService(string clientId)
    {
        var associations = MatrixLoader.Load(CsvLoader.Load("Data/Matrix"));
        var recipes = RecipeCsvParser.Parse(CsvLoader.Load(PlantConfig.GetCsvPath(clientId)));
        RecipeValidator.Validate(recipes);
        potionService = new PotionService(associations, new RecipeRegistry(recipes));
    }

    public PotionData CreatePotion(string a, string b, string c)
    {
        return potionService.CreatePotion(a, b, c);
    }
}