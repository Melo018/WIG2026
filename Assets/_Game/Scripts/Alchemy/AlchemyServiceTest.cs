
using UnityEngine;

public class AlchemyServiceTest : MonoBehaviour
{
    void Start()
    {
        AlchemyService alchemy = new AlchemyService("A");
        PotionData potion = alchemy.CreatePotion("BA", "UA", "UB");
        Debug.Log("Tags : " + string.Join(", ", potion.Tags));
        Debug.Log("Recette trouvée : " + potion.RecipeFound);
        Debug.Log("Résultat : " + (potion.RecipeFound ? potion.RecipeResult : "Recette inconnue"));
    }
}