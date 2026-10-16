
using UnityEngine;

public class MatrixLoaderTest : MonoBehaviour
{
    void Start()
    {
        var associations = MatrixLoader.Load(CsvLoader.Load("Data/Matrix"));
        Debug.Log("Associations chargées : " + associations.Count);
        Debug.Log("BA + UA : " + string.Join(", ", associations[MatrixLoader.GetKey("BA", "UA")]));
    }
}