using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Helper
{
    public static class HelperFunctions
    {
        /// <summary>
        /// Downloads a file from the specified URL asynchronously
        /// </summary>
        /// <param name="url">The URL of the file to download</param>
        /// <returns>Byte array containing the downloaded data</returns>
        public static async Task<byte[]> DownloadFileAsync(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    byte[] fileData = await client.GetByteArrayAsync(url);
                    return fileData;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error downloading file from {url}: {ex.Message}");

                return null;
            }
        }

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

                return modelData;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error downloading 3D model: {ex.Message}");
                return null;
            }
        }
    }
}