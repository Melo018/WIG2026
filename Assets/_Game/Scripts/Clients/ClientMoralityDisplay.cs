
using UnityEngine;
using UnityEngine.UI;

public class ClientMoralityDisplay : MonoBehaviour
{
    [SerializeField] private ClientMorality clientMorality;
    [SerializeField] private Image catImage;

    private Sprite[] catSprites = new Sprite[7];
    private int displayedMorality = 999;

    private void Awake()
    {
        if (catImage == null)
            catImage = GetComponent<Image>();

        if (clientMorality == null)
            clientMorality = GetComponentInParent<ClientMorality>();

        catSprites[0] = Resources.Load<Sprite>("Art/Cat/cat-3");
        catSprites[1] = Resources.Load<Sprite>("Art/Cat/cat-2");
        catSprites[2] = Resources.Load<Sprite>("Art/Cat/cat-1");
        catSprites[3] = Resources.Load<Sprite>("Art/Cat/cat");
        catSprites[4] = Resources.Load<Sprite>("Art/Cat/cat+1");
        catSprites[5] = Resources.Load<Sprite>("Art/Cat/cat+2");
        catSprites[6] = Resources.Load<Sprite>("Art/Cat/cat+3");

        for (int i = 0; i < catSprites.Length; i++)
        {
            if (catSprites[i] == null)
                Debug.LogError("Sprite de chat introuvable à l'index " + i + ". Vérifie Assets/_Game/Resources/Art/Cat.");
        }

        if (clientMorality == null)
            Debug.LogError("ClientMorality introuvable dans les parents de " + name);
    }

    private void OnEnable()
    {
        displayedMorality = 999;
        UpdateDisplay();
    }

    private void Update()
    {
        if (clientMorality != null &&
            displayedMorality != clientMorality.Morality)
        {
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        if (clientMorality == null || catImage == null)
            return;

        int morality = Mathf.Clamp(clientMorality.Morality, -3, 3);
        int index = morality + 3;

        if (catSprites[index] == null)
            return;

        catImage.sprite = catSprites[index];
        displayedMorality = morality;
    }
}