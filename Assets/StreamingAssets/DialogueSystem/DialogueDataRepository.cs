
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DialogueDataRepository : MonoBehaviour
{
    private const string ProfilePath =
        "DialogueSystem/content_profile.json";

    public ContentProfile ContentProfile { get; private set; }

    public IEnumerator LoadContentProfile(
        Action<bool, string> onComplete)
    {
        string path = Path.Combine(
            Application.streamingAssetsPath,
            ProfilePath
        );

        using (UnityWebRequest request = UnityWebRequest.Get(path))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                string error =
                    "Impossible de charger le profil : "
                    + request.error;

                Debug.LogError(error);
                onComplete?.Invoke(false, error);
                yield break;
            }

            try
            {
                ContentProfile profile =
                    JsonUtility.FromJson<ContentProfile>(
                        request.downloadHandler.text
                    );

                if (profile == null
                    || string.IsNullOrWhiteSpace(profile.profileId)
                    || string.IsNullOrWhiteSpace(profile.versionMode))
                {
                    string error = "Le profil de contenu est invalide.";
                    Debug.LogError(error);
                    onComplete?.Invoke(false, error);
                    yield break;
                }

                ContentProfile = profile;
                Debug.Log(
                    "Profil chargé : " + ContentProfile.profileId
                    + " | Mode : " + ContentProfile.versionMode
                );

                onComplete?.Invoke(true, null);
            }
            catch (Exception exception)
            {
                string error =
                    "Erreur de lecture du profil : "
                    + exception.Message;

                Debug.LogError(error);
                onComplete?.Invoke(false, error);
            }
        }
    }
}