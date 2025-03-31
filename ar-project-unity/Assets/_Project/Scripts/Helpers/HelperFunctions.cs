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
    }
}