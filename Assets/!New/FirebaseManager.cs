using System.Collections;
using Firebase;
using Firebase.Analytics;
using Firebase.Crashlytics;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }
    private FirebaseApp app;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        StartCoroutine(InitializeFirebase());
#endif
    }

    private IEnumerator InitializeFirebase()
    {
        Debug.LogError("InitializeFirebase");
        var dependencyStatus = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => dependencyStatus.IsCompleted);

        if (dependencyStatus.Result == DependencyStatus.Available)
        {
            Debug.LogError("Firebase is ready!");
            app = FirebaseApp.DefaultInstance;

            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            SendGameStart();
            SetCustomKeys();
        }
        else
        {
            Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus.Result);
        }
    }

    private void SetCustomKeys()
    {
        Crashlytics.SetUserId(PlayerPrefs.GetInt("user_id", 0).ToString());
        Crashlytics.SetCustomKey("user_level", PlayerPrefs.GetInt("user_level").ToString());
        Crashlytics.SetCustomKey("deviceId", SystemInfo.deviceUniqueIdentifier);
        Crashlytics.SetCustomKey("deviceName", SystemInfo.deviceName);
        Crashlytics.SetCustomKey("deviceType", SystemInfo.deviceType.ToString());
        Crashlytics.SetCustomKey("deviceModel", SystemInfo.deviceModel);
        Crashlytics.SetCustomKey("systemMemorySize", SystemInfo.systemMemorySize.ToString());
        Crashlytics.SetCustomKey("graphicsMemorySize", SystemInfo.graphicsMemorySize.ToString());
        Crashlytics.SetCustomKey("isKnownQADevice", IsKnownQADevice().ToString());
    }

    private bool IsKnownQADevice()
    {
        // Check if ListOfKnownQADevices contains this SystemInfo.deviceUniqueIdentifier
        return false;
    }
    
    public void SendGameStart()
    {
        Debug.LogError("SendGameStart");
        FirebaseAnalytics.LogEvent("game_start");
        //FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
    }

    public void SendLevelStart(int useLevel)
    {
        Debug.LogError("SendLevelStart");
        Parameter[] levelStartParameters =
        {
            new(FirebaseAnalytics.ParameterLevel, useLevel)
        };
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart, levelStartParameters);
    }

    public void SendLevelEnd(int useLevel, string winner)
    {
        Debug.LogError("SendLevelEnd");
        Parameter[] levelEndParameters =
        {
            new(FirebaseAnalytics.ParameterLevel, useLevel),
            new("winner", winner)
        };
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd, levelEndParameters);
    }

    public void SetLevel(int useLevel)
    {
        Crashlytics.SetCustomKey("user_level",useLevel.ToString());
    }
}