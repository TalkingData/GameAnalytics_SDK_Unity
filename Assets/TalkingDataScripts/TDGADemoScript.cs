using UnityEngine;
using System.Collections.Generic;

public class TDGADemoScript : MonoBehaviour
{
    private const int top = 100;
    private const int left = 80;
    private const int height = 50;
    private readonly int width = Screen.width - (left * 2);
    private const int step = 60;
    private string deviceId;
    private string oaid;
    private TDGAProfile profile;
    private int index = 1;
    private int level = 1;

    private void OnGUI()
    {
        int i = 0;
        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Demo Menu");

        GUI.Label(new Rect(left, top + (step * i++), width, height), deviceId);
        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "getDeviceId"))
        {
            deviceId = TalkingDataGA.GetDeviceId();
        }

        GUI.Label(new Rect(left, top + (step * i++), width, height), oaid);
        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "getOAID"))
        {
            oaid = TalkingDataGA.GetOAID();
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "SetLocation"))
        {
            TalkingDataGA.SetLocation(39.94, 116.43);
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Create Profile"))
        {
            profile = TDGAProfile.SetProfile("User" + index++);
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Set Profile Name"))
        {
            if (profile != null)
            {
                profile.SetProfileName("name");
            }
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Set Profile Type"))
        {
            if (profile != null)
            {
                profile.SetProfileType(ProfileType.WEIXIN);
            }
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Set Level"))
        {
            if (profile != null)
            {
                profile.SetLevel(level++);
            }
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Set Gender"))
        {
            if (profile != null)
            {
                profile.SetGender(Gender.MALE);
            }
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Set Age"))
        {
            if (profile != null)
            {
                profile.SetAge(21);
            }
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Set Game Server"))
        {
            if (profile != null)
            {
                profile.SetGameServer("server1");
            }
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Mission Begin"))
        {
            TDGAMission.OnBegin("miss001");
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Mission Completed"))
        {
            TDGAMission.OnCompleted("miss001");
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Mission Failed"))
        {
            TDGAMission.OnFailed("miss001", "failed");
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Charge Request"))
        {
            TDGAVirtualCurrency.OnChargeRequest("order01", "iap", 10, "CNY", 10, "UnionPay");
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Charge Success"))
        {
            TDGAVirtualCurrency.OnChargeSuccess("order01");
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Reward"))
        {
            TDGAVirtualCurrency.OnReward(100, "reason");
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Item Purchase"))
        {
            TDGAItem.OnPurchase("itemid001", 10, 10);
        }

        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Item Use"))
        {
            TDGAItem.OnUse("itemid001", 1);
        }

#if TDGA_CUSTOM
        if (GUI.Button(new Rect(left, top + (step * i++), width, height), "Custome Event"))
        {
            Dictionary<string, object> dic = new Dictionary<string, object>
            {
                { "StringValue", "Pi" },
                { "NumberValue", 3.14 }
            };
            TalkingDataGA.OnEvent("action_id", dic);
        }
#endif
    }

    private void Start()
    {
        Debug.Log("Start");
        //TalkingDataGA.SetVerboseLogDisabled();
        TalkingDataGA.BackgroundSessionEnabled();
        TalkingDataGA.OnStart("your_app_id", "your_channel_id");
        profile = TDGAProfile.SetProfile("User" + index++);
#if TDGA_PUSH
#if UNITY_IPHONE
        UnityEngine.iOS.NotificationServices.RegisterForNotifications(
            UnityEngine.iOS.NotificationType.Alert |
            UnityEngine.iOS.NotificationType.Badge |
            UnityEngine.iOS.NotificationType.Sound);
#endif
#endif
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
#if TDGA_PUSH
        TalkingDataGA.SetDeviceToken();
        TalkingDataGA.HandlePushMessage();
#endif
    }

    private void OnDestroy()
    {
        Debug.Log("onDestroy");
        TalkingDataGA.OnEnd();
    }

    private void Awake()
    {
        Debug.Log("Awake");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }
}
