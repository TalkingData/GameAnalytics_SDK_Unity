using UnityEngine;
#if UNITY_IPHONE
using System.Runtime.InteropServices;
#endif


public enum ProfileType
{
    ANONYMOUS = 0,
    REGISTERED = 1,
    SINA_WEIBO = 2,
    QQ = 3,
    QQ_WEIBO = 4,
    ND91 = 5,
    WEIXIN = 6,
    TYPE1 = 11,
    TYPE2 = 12,
    TYPE3 = 13,
    TYPE4 = 14,
    TYPE5 = 15,
    TYPE6 = 16,
    TYPE7 = 17,
    TYPE8 = 18,
    TYPE9 = 19,
    TYPE10 = 20
}

public enum Gender
{
    UNKNOW = 0,
    MALE = 1,
    FEMALE = 2
}


public class TDGAProfile
{
    private static TDGAProfile profile;

#if UNITY_ANDROID
    private static readonly string PROFILE_CLASS = "com.tendcloud.tenddata.TDGAProfile";
    private static AndroidJavaClass profileClass;
    private AndroidJavaObject mProfile;
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void TDGASetProfile(string profileId);

    [DllImport("__Internal")]
    private static extern void TDGASetProfileName(string profileName);

    [DllImport("__Internal")]
    private static extern void TDGASetProfileType(int profileType);

    [DllImport("__Internal")]
    private static extern void TDGASetLevel(int level);

    [DllImport("__Internal")]
    private static extern void TDGASetGender(int gender);

    [DllImport("__Internal")]
    private static extern void TDGASetAge(int age);

    [DllImport("__Internal")]
    private static extern void TDGASetGameServer(string gameServer);
#endif

    public static TDGAProfile SetProfile(string profileId)
    {
        if (profile == null)
        {
            profile = new TDGAProfile();
        }
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (profileClass == null)
            {
                profileClass = new AndroidJavaClass(PROFILE_CLASS);
            }
            profile.mProfile = profileClass.CallStatic<AndroidJavaObject>("setProfile", profileId);
#endif
#if UNITY_IPHONE
            TDGASetProfile(profileId);
#endif
        }
        return profile;
    }

    public void SetProfileName(string profileName)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (mProfile != null)
            {
                mProfile.Call("setProfileName", profileName);
            }
#endif
#if UNITY_IPHONE
            TDGASetProfileName(profileName);
#endif
        }
    }

    public void SetProfileType(ProfileType type)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (mProfile != null)
            {
                AndroidJavaClass enumClass = new AndroidJavaClass("com.tendcloud.tenddata.TDGAProfile$ProfileType");
                AndroidJavaObject obj = enumClass.CallStatic<AndroidJavaObject>("valueOf", type.ToString());
                mProfile.Call("setProfileType", obj);
                enumClass.Dispose();
            }
#endif
#if UNITY_IPHONE
            TDGASetProfileType((int)type);
#endif
        }
    }

    public void SetLevel(int level)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (mProfile != null)
            {
                mProfile.Call("setLevel", level);
            }
#endif
#if UNITY_IPHONE
            TDGASetLevel(level);
#endif
        }
    }

    public void SetAge(int age)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (mProfile != null)
            {
                mProfile.Call("setAge", age);
            }
#endif
#if UNITY_IPHONE
            TDGASetAge(age);
#endif
        }
    }

    public void SetGender(Gender type)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (mProfile != null)
            {
                AndroidJavaClass enumClass = new AndroidJavaClass("com.tendcloud.tenddata.TDGAProfile$Gender");
                AndroidJavaObject obj = enumClass.CallStatic<AndroidJavaObject>("valueOf", type.ToString());
                mProfile.Call("setGender", obj);
                enumClass.Dispose();
            }
#endif
#if UNITY_IPHONE
            TDGASetGender((int)type);
#endif
        }
    }

    public void SetGameServer(string gameServer)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (mProfile != null)
            {
                mProfile.Call("setGameServer", gameServer);
            }
#endif
#if UNITY_IPHONE
            TDGASetGameServer(gameServer);
#endif
        }
    }
}
