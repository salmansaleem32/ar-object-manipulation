using UnityEngine;
using System.Threading.Tasks;
using System;
using Helper;

public static class AssetDownloader
{
    /// <summary>
    /// Downloads a 3D model from the specified URL
    /// </summary>
    /// <param name="url">The URL of the 3D model to download</param>
    /// <returns>Returns the downloaded bytes of the 3D model file</returns>
    public static async Task<byte[]> Download3DModelAsync(string url)
    {
        try
        {
            byte[] modelData = await HelperFunctions.DownloadFileAsync(url);
            
            if (modelData == null || modelData.Length == 0)
            {
                Debug.LogError($"Failed to download 3D model from URL: {url}");
                return null;
            }

            Debug.Log($"Successfully downloaded 3D model from URL: {url}");
            return modelData;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error downloading 3D model: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Downloads an image from the specified URL
    /// </summary>
    /// <param name="url">The URL of the image to download</param>
    /// <returns>Returns the downloaded bytes of the image file</returns>
    public static async Task<Texture2D> DownloadImageAsync(string url)
    {
        try
        {
            byte[] imageData = await HelperFunctions.DownloadFileAsync(url);
            
            if (imageData == null || imageData.Length == 0)
            {
                Debug.LogError($"Failed to download image from URL: {url}");
                return null;
            }

            Texture2D texture = new Texture2D(2, 2);
            if (!texture.LoadImage(imageData))
            {
                Debug.LogError($"Failed to convert image data to texture from URL: {url}");
                return null;
            }

            Debug.Log($"Successfully downloaded image from URL: {url}");
            return texture;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error downloading image: {ex.Message}");
            return null;
        }
    }
}
