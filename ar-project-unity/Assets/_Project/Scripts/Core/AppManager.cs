using UnityEngine;

public class AppManager : MonoBehaviour
{
    private static AppManager instance;

    public static AppManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<AppManager>();
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