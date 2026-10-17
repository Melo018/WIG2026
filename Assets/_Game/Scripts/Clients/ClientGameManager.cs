
using System.Collections.Generic;
using UnityEngine;

public class ClientGameManager : MonoBehaviour
{
    [System.Serializable]
    public class ClientEntry
    {
        public string clientId;
        public GameObject clientObject;
    }

    [SerializeField] private ClientEntry[] clients;
    [SerializeField] private PotionCreationController potionCreationController;
    [SerializeField] private int victoryThreshold = 3;

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

            entry.clientObject.SetActive(false);
            waitingClients.Enqueue(entry);
        }

        ShowNextClient();
    }

    public void ShowNextClient()
    {
        if (gameEnded)
            return;

        if (currentClient != null && currentClient.clientObject != null)
            currentClient.clientObject.SetActive(false);

        currentClient = null;

        while (waitingClients.Count > 0)
        {
            ClientEntry next = waitingClients.Dequeue();

            if (next.clientObject == null)
                continue;

            currentClient = next;
            currentClient.clientObject.SetActive(true);

            if (potionCreationController != null)
                potionCreationController.SetClient(currentClient.clientId);

            Debug.Log("Client actif : " + currentClient.clientId);
            return;
        }

        EndGame();
    }

    public void ReturnCurrentClientToQueue()
    {
        if (gameEnded || currentClient == null)
            return;

        currentClient.clientObject.SetActive(false);
        waitingClients.Enqueue(currentClient);

        Debug.Log("Client remis en file : " + currentClient.clientId);

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

        Debug.Log("Clients sauvés : " + savedClients + " | Clients perdus : " + lostClients);

        currentClient.clientObject.SetActive(false);
        currentClient = null;

        ShowNextClient();
    }

    private void EndGame()
    {
        gameEnded = true;

        bool victory = savedClients >= victoryThreshold;

        Debug.Log(victory
            ? "VICTOIRE — au moins " + victoryThreshold + " clients sauvés."
            : "DÉFAITE — seulement " + savedClients + " clients sauvés.");
    }
}