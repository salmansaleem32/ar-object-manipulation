using UnityEngine;
using System.Collections;
using System.IO;

public class NativeShareHandler : MonoBehaviour
{
    public static NativeShareHandler Instance { get; private set; }

    [Header("Configuration")]
    public string screenshotPrefix = "screenshot_";
    public string fileExtension = ".png";
    public string trackingURL = "https://mygame.com/?ref=share";

    [Header("Debug")]
    [SerializeField] private Texture2D lastCapturedScreenshot;
    public System.Action<bool> OnShareCompleted; // Callback for UI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CaptureAndShare(string shareMessage = "")
    {
        StartCoroutine(CaptureScreenAndShare(shareMessage));
    }

    private IEnumerator CaptureScreenAndShare(string message)
    {
        yield return new WaitForEndOfFrame();

        // Capture screen
        lastCapturedScreenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        lastCapturedScreenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        lastCapturedScreenshot.Apply();

        // Save temporary file
        string filename = screenshotPrefix + System.DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
        string filepath = Path.Combine(Application.persistentDataPath, filename);
        File.WriteAllBytes(filepath, lastCapturedScreenshot.EncodeToPNG());

        // Prepare share message
        string fullMessage = string.IsNullOrEmpty(message) ?
            $"Check out my game! {trackingURL}" :
            $"{message} {trackingURL}";

        // Native share with callback
        new NativeShare()
            .AddFile(filepath)
            .SetText(fullMessage)
            .SetSubject("Game Screenshot")
            .SetCallback((result, shareTarget) => {
                bool success = result == NativeShare.ShareResult.Shared;
                OnShareCompleted?.Invoke(success);
            })
            .Share();

        StartCoroutine(DeleteFileAfterDelay(filepath, 60f));
    }

    public Texture2D GetLastScreenshot() => lastCapturedScreenshot;

    private IEnumerator DeleteFileAfterDelay(string path, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (File.Exists(path)) File.Delete(path);
    }
}