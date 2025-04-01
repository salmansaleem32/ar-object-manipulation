using System;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    internal void Initialise()
    {
        DownloadImages();
    }

    private async Task DownloadImages()
    {
        foreach (var item in AppManager.Instance.ItemsMetadataList)
        {
            string imageUrl = item.imageUrl;
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var texture = await AssetDownloader.DownloadImageAsync(imageUrl);

                if (texture != null)
                {
                    item.SetTexture(texture); // Update the original item in the list
                    Debug.Log($"Image downloaded and assigned for {item.name}");
                }
                else
                {
                    Debug.LogError($"Failed to download image for {item.name}");
                }
            }
            else
            {
                Debug.LogError($"Image URL not found for {item.name}");
            }
        }
    }
}
