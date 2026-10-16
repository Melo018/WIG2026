
using UnityEngine;

public class PotionTagCalculatorTest : MonoBehaviour
{
    void Start()
    {
        var associations = MatrixLoader.Load(CsvLoader.Load("Data/Matrix"));
        var tags = PotionTagCalculator.Calculate("BA", "UA", "UB", associations);
        Debug.Log("Tags de la potion : " + string.Join(", ", tags));

        var reversedTags = PotionTagCalculator.Calculate("UB", "BA", "UA", associations);
        if (string.Join("|", tags) != string.Join("|", reversedTags))
            throw new System.Exception("Le résultat change selon l'ordre des ingrédients.");

        Debug.Log("Test d'ordre réussi.");
    }
}