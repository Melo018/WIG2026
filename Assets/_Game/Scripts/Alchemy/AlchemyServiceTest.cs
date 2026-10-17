
using UnityEngine;

public class AlchemyServiceTest : MonoBehaviour
{
    void Start()
    {
        TestClient("A");
        TestClient("B");
        TestClient("C");
        TestClient("D");
    }

    void TestClient(string clientId)
    {
        AlchemyService alchemy = new AlchemyService(clientId);
        PotionData potion = alchemy.CreatePotion("BA", "UA", "UB");

        Debug.Log("Client " + clientId + " | Tags : " + string.Join(", ", potion.Tags) + " | Recette trouvée : " + potion.RecipeFound + " | Résultat : " + (potion.RecipeFound ? potion.RecipeResult : "Recette inexistante"));
    }
}