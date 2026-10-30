
using System.Collections.Generic;
using UnityEngine;

public class ClientGameManager : MonoBehaviour
{
    [System.Serializable]
    public class ClientEntry
    {
        public string clientId;
        public GameObject clientObject;
        public ClientVisual clientVisual;
    }

    [SerializeField] private ClientEntry[] clients;
    [SerializeField] private PotionCreationController potionCreationController;
    [SerializeField] private int victoryThreshold = 3;
    [SerializeField] private ClientDialogue clientDialogue;

    private readonly Queue<ClientEntry> waitingClients = new Queue<ClientEntry>();
    private ClientEntry currentClient;
    private int savedClients;
    private int lostClients;
    private bool gameEnded;

    private void Start()
    {
        waitingClients.Clear();

        foreach (ClientEntry entry in clients)
        {
            if (entry == null || entry.clientObject == null)
                continue;

            SetClientVisible(entry, false);
            waitingClients.Enqueue(entry);
        }

        ShowNextClient();
    }

    public void ShowNextClient()
    {
        if (gameEnded)
            return;

        if (currentClient != null)
            SetClientVisible(currentClient, false);

        currentClient = null;

        while (waitingClients.Count > 0)
        {
            ClientEntry next = waitingClients.Dequeue();

            if (next.clientObject == null)
                continue;

            currentClient = next;
            SetClientVisible(currentClient, true);

            if (potionCreationController != null)
                potionCreationController.SetClient(currentClient.clientId);

            PotionClient potionClient = currentClient.clientObject.GetComponent<PotionClient>();

            if (potionClient != null)
                potionClient.ShowMeetingDialogue();

            return;
        }

        EndGame();
    }

    public void ReturnCurrentClientToQueue()
    {
        if (gameEnded || currentClient == null)
            return;

        SetClientVisible(currentClient, false);
        waitingClients.Enqueue(currentClient);

        currentClient = null;
        ShowNextClient();
    }

    public void ResolveCurrentClient(bool saved)
    {
        if (gameEnded || currentClient == null)
            return;

        if (saved)
            savedClients++;
        else
            lostClients++;

        SetClientVisible(currentClient, false);
        currentClient = null;

        ShowNextClient();
    }

    private void SetClientVisible(ClientEntry entry, bool visible)
    {
        if (entry.clientVisual != null)
            entry.clientVisual.SetVisible(visible);
        if (entry.clientObject != null)
            entry.clientObject.SetActive(visible);
    }

    private void EndGame()
    {
        gameEnded = true;

        bool victory = savedClients >= victoryThreshold;
    }
}