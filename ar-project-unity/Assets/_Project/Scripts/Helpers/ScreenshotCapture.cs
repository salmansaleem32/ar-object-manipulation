using System;
using System.Collections;
using System.IO;
using UnityEngine;
using NativeShare.Scripts;

public static class ScreenshotCapture
{

    public static void CaptureScreenshot(string fileName, Action<string> onComplete)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("File name cannot be null or empty.");
            return;
        }

        // Start the coroutine to take a screenshot
        AppManager.Instance.StartCoroutine(TakeScreenshot(fileName, onComplete));
    }

    private static IEnumerator TakeScreenshot(string fileName, Action<string> onComplete)
    {
        yield return new WaitForEndOfFrame();

        string filePath = Path.Combine(Application.temporaryCachePath, fileName);
        ScreenCapture.CaptureScreenshot(fileName);

        // Wait until the file is written
        while (!File.Exists(filePath)) yield return null;

        onComplete?.Invoke(filePath);
    }

    public static void ShareScreenshot(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            Debug.LogError("File path is invalid or file does not exist.");
            return;
        }

        // Implement sharing functionality here, e.g., using NativeShare or any other sharing library
        Debug.Log($"Sharing screenshot: {filePath}");
        NativeShare.Share("Check out this screenshot!", "", filePath, "");

    }
}
