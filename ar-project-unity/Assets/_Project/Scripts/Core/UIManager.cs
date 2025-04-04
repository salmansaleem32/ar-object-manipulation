using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class UIManager : MonoBehaviour
{

    [SerializeField] private List<Button> objectButtons;
    [SerializeField] private Button shareButton;

    ObjectSpawner _objectSpawner;
    internal async void Initialise()
    {
        Debug.Log("UIManager Initialised");

        await AssetDownloader.DownloadImages(AppManager.Instance.ItemsMetadataList);

        foreach (var item in AppManager.Instance.ItemsMetadataList)
        {

            int index = AppManager.Instance.ItemsMetadataList.IndexOf(item);
            if (index < objectButtons.Count)
            {
                var buttonImage = objectButtons[index].GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.sprite = Sprite.Create(item.image, new Rect(0, 0, item.image.width, item.image.height), new Vector2(0.5f, 0.5f));
                    objectButtons[index].onClick.AddListener(() => OnButtonClick(item.name));
                }
            }
        }
        shareButton.onClick.AddListener(OnClickShareButton);

        await AssetDownloader.Download3DModels(AppManager.Instance.ItemsMetadataList);
        _objectSpawner = AppManager.Instance.objectSpawner;
    }

    private async void OnButtonClick (string objectKey)
    {
        var modelToInstantiate = AppManager.Instance.ItemsMetadataList.Find(x => x.name == objectKey).model;
        
        
         // Create a container GameObject
            GameObject root = new GameObject("objectKey");
            bool success = await modelToInstantiate.InstantiateMainSceneAsync(root.transform);

            if (!success)
            {
                GameObject.Destroy(root);
                Debug.LogError($"Failed to instantiate GLB model from URL: {objectKey}");
            }
    }

    private void OnClickShareButton()
    {
        // Implement share functionality here
        Debug.Log("Share button clicked!");
    }
    
}
