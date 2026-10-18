
using UnityEngine;

public class CsvLoaderTest : MonoBehaviour
{
    void Start()
    {
        string csv = CsvLoader.Load("Data/Matrix");
        Debug.Log(csv);
    }
}