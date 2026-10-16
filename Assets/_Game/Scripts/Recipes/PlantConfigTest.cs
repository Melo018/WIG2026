
using UnityEngine;

public class PlantConfigTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log(PlantConfig.GetCsvPath("A"));
        Debug.Log(PlantConfig.GetCsvPath("B"));
        Debug.Log(PlantConfig.GetCsvPath("C"));
        Debug.Log(PlantConfig.GetCsvPath("D"));
    }
}