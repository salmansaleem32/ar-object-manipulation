using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AppManager : MonoBehaviour
{
    private static AppManager instance;

    public static AppManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Object.FindFirstObjectByType<AppManager>(); // Updated method
                if (instance == null)
                {
                    GameObject obj = new GameObject("AppManager");
                    instance = obj.AddComponent<AppManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeApp();
    }

    private void InitializeApp()
    {
        Debug.Log("App Initialized");
        // Add initialization logic here
    }

    public void DownloadAssets(string[] urls)
    {
        if (urls.Length != 5)
        {
            Debug.LogError("Please provide exactly 5 URLs.");
            return;
        }

        StartCoroutine(DownloadAssetsCoroutine(urls));
    }

    private IEnumerator DownloadAssetsCoroutine(string[] urls)
    {
        for (int i = 0; i < urls.Length; i++)
        {
            string url = urls[i];
            Debug.Log($"Downloading asset {i + 1} from: {url}");

            yield return DownloadFileAsync(url, i + 1);
        }

        Debug.Log("All assets downloaded.");
    }

    private IEnumerator DownloadFileAsync(string url, int assetIndex)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Asset {assetIndex} downloaded successfully: {url}");
                // Process the downloaded data if needed
                byte[] data = request.downloadHandler.data;
            }
            else
            {
                Debug.LogError($"Failed to download asset {assetIndex} from: {url}. Error: {request.error}");
            }
        }
    }

    void OnApplicationQuit()
    {
        CleanupApp();
    }

    private void CleanupApp()
    {
        Debug.Log("App Cleanup");
        // Add cleanup logic here
    }
}
