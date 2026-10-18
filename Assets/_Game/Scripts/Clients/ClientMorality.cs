
using UnityEngine;

public class ClientMorality : MonoBehaviour
{
    [SerializeField, Range(-3, 3)]
    private int morality = 0;

    public int Morality => morality;

    public bool IsNoble => morality > 0;
    public bool IsCursed => morality < 0;

    public bool ApplyPotionResult(string result)
    {
        switch (result?.ToLowerInvariant())
        {
            case "parfait":
            case "bon":
                morality = Mathf.Min(morality + 2, 3);
                break;

            case "change":
            case "null":
                morality = Mathf.Max(morality - 1, -3);
                break;

            default:
                Debug.LogWarning("Résultat de potion inconnu : " + result);
                return false;
        }

        return morality == 3 || morality == -3;
    }
}