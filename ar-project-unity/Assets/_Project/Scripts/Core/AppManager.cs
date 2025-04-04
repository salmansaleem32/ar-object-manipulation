using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;

public class AppManager : MonoBehaviour
{
    #region Variables and Declarations
    private static AppManager instance;
    public UIManager uIManager;
    public List<ItemMetadata> ItemsMetadataList { get; private set; }

public ObjectSpawner objectSpawner;
public ARInteractorSpawnTrigger arInteractorSpawnTrigger;

    #endregion

#region Properties and Dictionaries

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

    private readonly Dictionary<string, string> modelsUrls = new Dictionary<string, string>()
    {
        { "Avocado", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/main/2.0/Avocado/glTF-Binary/Avocado.glb" },
        { "Buggy", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/main/2.0/Buggy/glTF-Binary/Buggy.glb" },
        { "DamagedHelmet", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/main/2.0/DamagedHelmet/glTF-Binary/DamagedHelmet.glb" },
        { "Duck", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/main/2.0/Duck/glTF-Binary/Duck.glb" },
        { "SheenChair", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/main/2.0/SheenChair/glTF-Binary/SheenChair.glb" }
    };

    private readonly Dictionary<string, string> imageUrls = new Dictionary<string, string>()
    {
        { "Avocado", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/main/2.0/Avocado/screenshot/screenshot.jpg" },
        { "Buggy", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/main/2.0/Buggy/screenshot/screenshot.png" },
        { "DamagedHelmet", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/main/2.0/DamagedHelmet/screenshot/screenshot.png" },
        { "Duck", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/main/2.0/Duck/screenshot/screenshot.png" },
        { "SheenChair", "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/main/2.0/SheenChair/screenshot/screenshot.jpg" }
    };

#endregion

 
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Initialize the app when it starts
        InitializeApp();
    }


    private void InitializeApp()
    {
        Debug.Log("App Initialized");
        // Add initialization logic here

        ItemsMetadataList = modelsUrls.Keys
            .Select(key => new ItemMetadata 
            { 
                name = key,
                imageUrl = imageUrls[key],
                modelUrl = modelsUrls[key]
            })
            .ToList();

            uIManager = FindFirstObjectByType<UIManager>();
            uIManager.Initialise();
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
