using UnityEngine;
using System.Collections.Generic;
#if UNITY_ANDROID
using System;
#endif
#if UNITY_IPHONE
using System.Runtime.InteropServices;
using System.Collections;
#endif


public static class TalkingDataGA
{
#if UNITY_ANDROID
    private static readonly string GAME_ANALYTICS_CLASS = "com.tendcloud.tenddata.TalkingDataGA";
    private static AndroidJavaClass gameAnalyticsClass;
    private static AndroidJavaClass unityPlayerClass;
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern string TDGAGetDeviceId();

    [DllImport("__Internal")]
    private static extern void TDGASetVerboseLogDisabled();

    [DllImport("__Internal")]
    private static extern void TDGABackgroundSessionEnabled();

    [DllImport("__Internal")]
    private static extern void TDGAOnStart(string appId, string channelId);

    [DllImport("__Internal")]
    private static extern void TDGASetLocation(double latitude, double longitude);

#if TDGA_CUSTOM
    [DllImport("__Internal")]
    private static extern void TDGAOnEvent(string eventId, string parameters);
#endif

#if TDGA_PUSH
    [DllImport("__Internal")]
    private static extern void TDGASetDeviceToken(byte[] deviceToken, int length);

    [DllImport("__Internal")]
    private static extern void TDGAHandlePushMessage(string message);

    private static bool hasTokenBeenObtained = false;
#endif
#endif

#if UNITY_ANDROID
    private static AndroidJavaObject GetCurrentActivity()
    {
        if (unityPlayerClass == null)
        {
            unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        }
        AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        return activity;
    }
#endif

    private static string deviceId = null;
    public static string GetDeviceId()
    {
        if (deviceId == null && Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (gameAnalyticsClass == null)
            {
                gameAnalyticsClass = new AndroidJavaClass(GAME_ANALYTICS_CLASS);
            }
            deviceId = gameAnalyticsClass.CallStatic<string>("getDeviceId", GetCurrentActivity());
#endif
#if UNITY_IPHONE
            deviceId = TDGAGetDeviceId();
#endif
        }
        return deviceId;
    }

    private static string oaid = null;
    public static string GetOAID()
    {
        if (oaid == null && Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (gameAnalyticsClass == null)
            {
                gameAnalyticsClass = new AndroidJavaClass(GAME_ANALYTICS_CLASS);
            }
            oaid = gameAnalyticsClass.CallStatic<string>("getOAID", GetCurrentActivity());
#endif
        }
        return oaid;
    }

    public static void SetVerboseLogDisabled()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (gameAnalyticsClass == null)
            {
                gameAnalyticsClass = new AndroidJavaClass(GAME_ANALYTICS_CLASS);
            }
            gameAnalyticsClass.CallStatic("setVerboseLogDisabled");
#endif
#if UNITY_IPHONE
            TDGASetVerboseLogDisabled();
#endif
        }
    }

    public static void BackgroundSessionEnabled()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
            TDGABackgroundSessionEnabled();
#endif
        }
    }

    public static void OnStart(string appId, string channelId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
            Debug.Log("TalkingData Game Analytics Unity SDK.");
#if UNITY_ANDROID
            using (AndroidJavaClass dz = new AndroidJavaClass("com.tendcloud.tenddata.game.dz"))
            {
                dz.SetStatic("a", 2);
            }
            if (gameAnalyticsClass == null)
            {
                gameAnalyticsClass = new AndroidJavaClass(GAME_ANALYTICS_CLASS);
            }
            AndroidJavaObject activity = GetCurrentActivity();
            gameAnalyticsClass.CallStatic("init", activity, appId, channelId);
            gameAnalyticsClass.CallStatic("onResume", activity);
#endif
#if UNITY_IPHONE
            TDGAOnStart(appId, channelId);
#endif
        }
    }

    public static void OnEnd()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (gameAnalyticsClass != null)
            {
                gameAnalyticsClass.CallStatic("onPause", GetCurrentActivity());
                gameAnalyticsClass = null;
                unityPlayerClass = null;
            }
#endif
        }
    }

    public static void OnKill()
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (gameAnalyticsClass != null)
            {
                gameAnalyticsClass.CallStatic("onKill", GetCurrentActivity());
                gameAnalyticsClass = null;
                unityPlayerClass = null;
            }
#endif
        }
    }

    public static void SetLocation(double latitude, double longitude)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_IPHONE
            TDGASetLocation(latitude, longitude);
#endif
        }
    }

#if TDGA_CUSTOM
    public static void OnEvent(string actionId, Dictionary<string, object> parameters)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (gameAnalyticsClass != null)
            {
                if (parameters != null && parameters.Count > 0)
                {
                    int count = parameters.Count;
                    AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap", count);
                    IntPtr method_Put = AndroidJNIHelper.GetMethodID(map.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
                    object[] args = new object[2];
                    foreach (KeyValuePair<string, object> kvp in parameters)
                    {
                        args[0] = new AndroidJavaObject("java.lang.String", kvp.Key);
                        args[1] = typeof(string).IsInstanceOfType(kvp.Value)
                            ? new AndroidJavaObject("java.lang.String", kvp.Value)
                            : new AndroidJavaObject("java.lang.Double", "" + kvp.Value);
                        AndroidJNI.CallObjectMethod(map.GetRawObject(), method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                    gameAnalyticsClass.CallStatic("onEvent", actionId, map);
                    map.Dispose();
                }
                else
                {
                    gameAnalyticsClass.CallStatic("onEvent", actionId, null);
                }
            }
#endif
#if UNITY_IPHONE
            if (parameters != null && parameters.Count > 0)
            {
                string parameterStr = "{";
                foreach (KeyValuePair<string, object> kvp in parameters)
                {
                    if (kvp.Value is string)
                    {
                        parameterStr += "\"" + kvp.Key + "\":\"" + kvp.Value + "\",";
                    }
                    else
                    {
                        try
                        {
                            double tmp = System.Convert.ToDouble(kvp.Value);
                            parameterStr += "\"" + kvp.Key + "\":" + tmp + ",";
                        }
                        catch (System.Exception)
                        {
                        }
                    }
                }
                parameterStr = parameterStr.TrimEnd(',');
                parameterStr += "}";
                TDGAOnEvent(actionId, parameterStr);
            }
            else
            {
                TDGAOnEvent(actionId, null);
            }
#endif
        }
    }
#endif

#if TDGA_PUSH
    public static void SetDeviceToken()
    {
#if UNITY_IPHONE
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
            if (!hasTokenBeenObtained)
            {
                byte[] deviceToken = UnityEngine.iOS.NotificationServices.deviceToken;
                if (deviceToken != null)
                {
                    TDGASetDeviceToken(deviceToken, deviceToken.Length);
                    hasTokenBeenObtained = true;
                }
            }
        }
#endif
    }

    public static void HandlePushMessage()
    {
#if UNITY_IPHONE
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
            UnityEngine.iOS.RemoteNotification[] notifications = UnityEngine.iOS.NotificationServices.remoteNotifications;
            if (notifications != null)
            {
                UnityEngine.iOS.NotificationServices.ClearRemoteNotifications();
                foreach (UnityEngine.iOS.RemoteNotification rn in notifications)
                {
                    foreach (DictionaryEntry de in rn.userInfo)
                    {
                        if (de.Key.ToString().Equals("sign"))
                        {
                            string sign = de.Value.ToString();
                            TDGAHandlePushMessage(sign);
                        }
                    }
                }
            }
        }
#endif
    }
#endif
}
