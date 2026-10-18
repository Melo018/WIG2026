
using System.Collections.Generic;

public class AssociationData
{
    public string IngredientA;
    public string IngredientB;
    public List<string> Tags;

    public AssociationData(string ingredientA, string ingredientB, List<string> tags)
    {
        IngredientA = ingredientA;
        IngredientB = ingredientB;
        Tags = tags;
    }
}