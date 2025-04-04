using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShareUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button shareButton;
    [SerializeField] private GameObject screenshotPreviewPanel;
    [SerializeField] private Image screenshotPreviewImage;
    [SerializeField] private Text statusText;
    [SerializeField] private GameObject loadingIndicator;

    [Header("Messages")]
    [SerializeField] private string shareMessage = "Playing this awesome game!";
    [SerializeField] private string successMessage = "Shared successfully!";
    [SerializeField] private string failMessage = "Share failed";
    [SerializeField] private string capturingMessage = "Capturing screen...";
    [SerializeField] private string preparingMessage = "Preparing share...";

    [Header("Settings")]
    [SerializeField] private float previewDisplayTime = 3f;
    [SerializeField] private Color successColor = Color.green;
    [SerializeField] private Color failColor = Color.red;

    private void Start()
    {
        shareButton.onClick.AddListener(OnShareClicked);
        NativeShareHandler.Instance.OnShareCompleted += OnShareComplete;

        // Initialize UI
        if (screenshotPreviewPanel != null)
            screenshotPreviewPanel.SetActive(false);
        if (loadingIndicator != null)
            loadingIndicator.SetActive(false);
    }

    private void OnDestroy()
    {
        NativeShareHandler.Instance.OnShareCompleted -= OnShareComplete;
    }

    private void OnShareClicked()
    {
        UpdateStatus(capturingMessage, Color.white);
        if (loadingIndicator != null) loadingIndicator.SetActive(true);
        NativeShareHandler.Instance.CaptureAndShare(shareMessage);
    }

    private void OnShareComplete(bool success)
    {
        // Hide loading
        if (loadingIndicator != null) loadingIndicator.SetActive(false);

        // Show result
        UpdateStatus(success ? successMessage : failMessage,
                   success ? successColor : failColor);

        // Show preview if successful
        if (success) StartCoroutine(ShowScreenshotPreview());
    }

    private IEnumerator ShowScreenshotPreview()
    {
        if (screenshotPreviewPanel == null || screenshotPreviewImage == null) yield break;

        // Get and display screenshot
        Texture2D screenshot = NativeShareHandler.Instance.GetLastScreenshot();
        if (screenshot != null)
        {
            Sprite previewSprite = Sprite.Create(
                screenshot,
                new Rect(0, 0, screenshot.width, screenshot.height),
                new Vector2(0.5f, 0.5f)
            );
            screenshotPreviewImage.sprite = previewSprite;
            screenshotPreviewPanel.SetActive(true);
        }

        // Auto-hide after delay
        yield return new WaitForSeconds(previewDisplayTime);
        screenshotPreviewPanel.SetActive(false);
    }

    private void UpdateStatus(string message, Color color)
    {
        if (statusText == null) return;
        statusText.text = message;
        statusText.color = color;
    }
}