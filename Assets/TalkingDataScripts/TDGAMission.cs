using UnityEngine;
#if UNITY_IPHONE
using System.Runtime.InteropServices;
#endif


public static class TDGAMission
{
#if UNITY_ANDROID
    private static readonly string MISSION_CLASS = "com.tendcloud.tenddata.TDGAMission";
    private static AndroidJavaClass missionClass;
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void TDGAOnBegin(string missionId);

    [DllImport("__Internal")]
    private static extern void TDGAOnCompleted(string missionId);

    [DllImport("__Internal")]
    private static extern void TDGAOnFailed(string missionId, string failedCause);
#endif

    public static void OnBegin(string missionId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (missionClass == null)
            {
                missionClass = new AndroidJavaClass(MISSION_CLASS);
            }
            missionClass.CallStatic("onBegin", missionId);
#endif
#if UNITY_IPHONE
            TDGAOnBegin(missionId);
#endif
        }
    }

    public static void OnCompleted(string missionId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (missionClass == null)
            {
                missionClass = new AndroidJavaClass(MISSION_CLASS);
            }
            missionClass.CallStatic("onCompleted", missionId);
#endif
#if UNITY_IPHONE
            TDGAOnCompleted(missionId);
#endif
        }
    }

    public static void OnFailed(string missionId, string failedCause)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (missionClass == null)
            {
                missionClass = new AndroidJavaClass(MISSION_CLASS);
            }
            missionClass.CallStatic("onFailed", missionId, failedCause);
#endif
#if UNITY_IPHONE
            TDGAOnFailed(missionId, failedCause);
#endif
        }
    }
}
