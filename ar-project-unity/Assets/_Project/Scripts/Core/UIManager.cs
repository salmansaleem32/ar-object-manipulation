using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    internal async void Initialise()
    {
        Debug.Log("UIManager Initialised");

        // await AssetDownloader.DownloadImages(AppManager.Instance.ItemsMetadataList);
        // await AssetDownloader.Download3DModels(AppManager.Instance.ItemsMetadataList);
    }
}
