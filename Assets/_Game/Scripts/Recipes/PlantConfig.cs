
public static class PlantConfig
{
    public static string GetCsvPath(string clientId)
    {
        switch (clientId)
        {
            case "A": return "Plants/Plant_A";
            case "B": return "Plants/Plant_B";
            case "C": return "Plants/Plant_C";
            case "D": return "Plants/Plant_D";
            default: throw new System.ArgumentException("Client inconnu : " + clientId);
        }
    }
}