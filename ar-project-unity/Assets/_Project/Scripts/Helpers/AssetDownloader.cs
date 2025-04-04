using UnityEngine;
using System.Threading.Tasks;
using System;
using Helper;
using System.IO;
using GLTFast;
using Models;
using System.Collections.Generic;

public static class AssetDownloader
{
    /// <summary>
    /// Downloads a 3D model from the specified URL
    /// </summary>
    /// <param name="url">The URL of the 3D model to download</param>
    /// <returns>Returns the downloaded bytes of the 3D model file</returns>
    public static async Task<GameObject> DownloadGlBModel(string url)
    {
        try
        {
            byte[] modelData = await HelperFunctions.Download3DModelAsync(url);
            
            if (modelData == null || modelData.Length == 0)
            {
                Debug.LogError($"Failed to download 3D model from URL: {url}");
                return null;
            }

            // Create a temporary file to store the GLB data
            string tempPath = Path.Combine(Application.temporaryCachePath, "temp.glb");
            File.WriteAllBytes(tempPath, modelData);

            // Import GLB using GLTFast
            var gltf = new GLTFast.GltfImport();
            bool success = await gltf.Load(modelData, new Uri(tempPath));

            if (!success)
            {
                Debug.LogError($"Failed to load GLB model from URL: {url}");
                return null;
            }

            // Create a container GameObject
            GameObject root = new GameObject("GLB_Model");
            success = await gltf.InstantiateMainSceneAsync(root.transform);

            if (!success)
            {
                GameObject.Destroy(root);
                Debug.LogError($"Failed to instantiate GLB model from URL: {url}");
                return null;
            }

            // Clean up temporary file
            File.Delete(tempPath);

            Debug.Log($"Successfully downloaded and converted GLB model from URL: {url}");
            return root;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error processing GLB model: {ex.Message}");
            return null;
        }
    }

/// <summary>
/// Downloads images for all items in the metadata list
/// and assigns them to the respective items.
/// </summary>
/// <returns></returns>
     public static async Task DownloadImages(List<ItemMetadata> itemsMetadataList)
    {
        int i=0;
        foreach (var item in AppManager.Instance.ItemsMetadataList)
        {
            string imageUrl = item.imageUrl;
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var texture = await Helper.HelperFunctions.DownloadImageAsync(imageUrl);

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

            // return;
        }
    }

     public static async Task Download3DModels(List<ItemMetadata> itemsMetadataList)
    {
        foreach (var item in AppManager.Instance.ItemsMetadataList)
        {
            string modelUrl = item.modelUrl;
            if (!string.IsNullOrEmpty(modelUrl))
            {
                var model = await AssetDownloader.DownloadGlBModel(modelUrl);
                
                if (model != null)
                {
                    item.SetModel(model);
                    Debug.Log($"3D model downloaded and assigned for {item.name}");
                }
                else
                {
                    Debug.LogError($"Failed to download 3D model for {item.name}");
                }
            }
            else
            {
                Debug.LogError($"Model URL not found for {item.name}");
            }
        }
    }
}
