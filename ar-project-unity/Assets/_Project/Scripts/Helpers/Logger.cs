using UnityEngine;

namespace Helper
{
    public class Logger
    {
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public static void LogError(string message)
        {
            Debug.LogError(message);
        }

        public static void LogException(System.Exception exception)
        {
            Debug.LogException(exception);
        }
        public static void LogFormat(string format, params object[] args)
        {
            Debug.LogFormat(format, args);
        }
}
}
