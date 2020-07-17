using UnityEngine;
#if UNITY_IPHONE
using System.Runtime.InteropServices;
#endif


public static class TDGAVirtualCurrency
{
#if UNITY_ANDROID
    private static readonly string VIRTUAL_CURRENCY_CLASS = "com.tendcloud.tenddata.TDGAVirtualCurrency";
    private static AndroidJavaClass virtualCurrencyClass;
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void TDGAOnChargeRequst(string orderId, string iapId, double currencyAmount, string currencyType, double virtualCurrencyAmount, string paymentType);

    [DllImport("__Internal")]
    private static extern void TDGAOnChargSuccess(string orderId);

    [DllImport("__Internal")]
    private static extern void TDGAOnReward(double virtualCurrencyAmount, string reason);
#endif

    public static void OnChargeRequest(string orderId, string iapId, double currencyAmount, string currencyType, double virtualCurrencyAmount, string paymentType)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (virtualCurrencyClass == null)
            {
                virtualCurrencyClass = new AndroidJavaClass(VIRTUAL_CURRENCY_CLASS);
            }
            virtualCurrencyClass.CallStatic("onChargeRequest", orderId, iapId, currencyAmount, currencyType, virtualCurrencyAmount, paymentType);
#endif
#if UNITY_IPHONE
            TDGAOnChargeRequst(orderId, iapId, currencyAmount, currencyType, virtualCurrencyAmount, paymentType);
#endif
        }
    }

    public static void OnChargeSuccess(string orderId)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (virtualCurrencyClass == null)
            {
                virtualCurrencyClass = new AndroidJavaClass(VIRTUAL_CURRENCY_CLASS);
            }
            virtualCurrencyClass.CallStatic("onChargeSuccess", orderId);
#endif
#if UNITY_IPHONE
            TDGAOnChargSuccess(orderId);
#endif
        }
    }

    public static void OnReward(double virtualCurrencyAmount, string reason)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (virtualCurrencyClass == null)
            {
                virtualCurrencyClass = new AndroidJavaClass(VIRTUAL_CURRENCY_CLASS);
            }
            virtualCurrencyClass.CallStatic("onReward", virtualCurrencyAmount, reason);
#endif
#if UNITY_IPHONE
            TDGAOnReward(virtualCurrencyAmount, reason);
#endif
        }
    }
}
