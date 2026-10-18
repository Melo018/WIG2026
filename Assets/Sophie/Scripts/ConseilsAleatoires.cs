using UnityEngine;
using TMPro;

public class ConseilsAleatoire : MonoBehaviour
{
    [SerializeField] TMP_Text texte;
    [SerializeField, TextArea] string[] conseils;

    void OnEnable()
    {
        texte.text = conseils[Random.Range(0, conseils.Length)];
    }
}