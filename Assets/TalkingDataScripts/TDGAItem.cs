using UnityEngine;
#if UNITY_IPHONE
using System.Runtime.InteropServices;
#endif


public static class TDGAItem
{
#if UNITY_ANDROID
    private static readonly string ITEM_CLASS = "com.tendcloud.tenddata.TDGAItem";
    private static AndroidJavaClass itemClass;
#endif

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void TDGAOnPurchase(string item, int itemNumber, double priceInVirtualCurrency);

    [DllImport("__Internal")]
    private static extern void TDGAOnUse(string item, int itemNumber);
#endif

    public static void OnPurchase(string item, int itemNumber, double priceInVirtualCurrency)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (itemClass == null)
            {
                itemClass = new AndroidJavaClass(ITEM_CLASS);
            }
            itemClass.CallStatic("onPurchase", item, itemNumber, priceInVirtualCurrency);
#endif
#if UNITY_IPHONE
            TDGAOnPurchase(item, itemNumber, priceInVirtualCurrency);
#endif
        }
    }

    public static void OnUse(string item, int itemNumber)
    {
        if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.WindowsEditor)
        {
#if UNITY_ANDROID
            if (itemClass == null)
            {
                itemClass = new AndroidJavaClass(ITEM_CLASS);
            }
            itemClass.CallStatic("onUse", item, itemNumber);
#endif
#if UNITY_IPHONE
            TDGAOnUse(item, itemNumber);
#endif
        }
    }
}
