using UnityEngine;
#if UNITY_IPHONE
using System.Runtime.InteropServices;
#endif


public enum AccountType
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


public class TDGAAccount
{
    private static TDGAAccount account;

#if UNITY_ANDROID
    private static readonly string ACCOUNT_CLASS = "com.tendcloud.tenddata.TDGAAccount";
    private static AndroidJavaClass accountClass;
    private AndroidJavaObject mAccount;
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void TDGASetAccount(string accountId);

    [DllImport("__Internal")]
    private static extern void TDGASetAccountName(string accountName);

    [DllImport("__Internal")]
    private static extern void TDGASetAccountType(int accountType);

    [DllImport("__Internal")]
    private static extern void TDGASetLevel(int level);

    [DllImport("__Internal")]
    private static extern void TDGASetGender(int gender);

    [DllImport("__Internal")]
    private static extern void TDGASetAge(int age);

    [DllImport("__Internal")]
    private static extern void TDGASetGameServer(string gameServer);
#endif

    public static TDGAAccount SetAccount(string accountId)
    {
        if (account == null)
        {
            account = new TDGAAccount();
        }
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (accountClass == null)
            {
                accountClass = new AndroidJavaClass(ACCOUNT_CLASS);
            }
            account.mAccount = accountClass.CallStatic<AndroidJavaObject>("setAccount", accountId);
#endif
#if UNITY_IPHONE
            TDGASetAccount(accountId);
#endif
        }
        return account;
    }

    public void SetAccountName(string accountName)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (mAccount != null)
            {
                mAccount.Call("setAccountName", accountName);
            }
#endif
#if UNITY_IPHONE
            TDGASetAccountName(accountName);
#endif
        }
    }

    public void SetAccountType(AccountType type)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (mAccount != null)
            {
                AndroidJavaClass enumClass = new AndroidJavaClass("com.tendcloud.tenddata.TDGAAccount$AccountType");
                AndroidJavaObject obj = enumClass.CallStatic<AndroidJavaObject>("valueOf", type.ToString());
                mAccount.Call("setAccountType", obj);
                enumClass.Dispose();
            }
#endif
#if UNITY_IPHONE
            TDGASetAccountType((int)type);
#endif
        }
    }

    public void SetLevel(int level)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (mAccount != null)
            {
                mAccount.Call("setLevel", level);
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
            if (mAccount != null)
            {
                mAccount.Call("setAge", age);
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
            if (mAccount != null)
            {
                AndroidJavaClass enumClass = new AndroidJavaClass("com.tendcloud.tenddata.TDGAAccount$Gender");
                AndroidJavaObject obj = enumClass.CallStatic<AndroidJavaObject>("valueOf", type.ToString());
                mAccount.Call("setGender", obj);
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
            if (mAccount != null)
            {
                mAccount.Call("setGameServer", gameServer);
            }
#endif
#if UNITY_IPHONE
            TDGASetGameServer(gameServer);
#endif
        }
    }
}
