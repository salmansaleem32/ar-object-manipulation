using UnityEngine;

public static class SharedConstants
{
   
    
    public static string GetStoreURL() =>
#if UNITY_ANDROID
        $"https://play.google.com/store/apps/details?id={Application.identifier}";
#elif UNITY_IOS
        $"https://apps.apple.com/app/id{Application.identifier}";
#else
        string.Empty;
#endif
}