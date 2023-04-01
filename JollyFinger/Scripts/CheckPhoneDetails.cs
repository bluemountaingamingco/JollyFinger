using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckPhoneDetails
{
    public static float GetPhoneDPI()
    {
        AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject metrics = new AndroidJavaObject("android.util.DisplayMetrics");
        activity.Call<AndroidJavaObject>("getWindowManager").Call<AndroidJavaObject>("getDefaultDisplay").Call("getMetrics", metrics);

        return (metrics.Get<float>("xdpi") + metrics.Get<float>("ydpi")) * 0.5f;
    }

    //public static float GetExpirementDPI()
    //{
    //    return (GetPhoneDPI() + Screen.dpi) / 2;
    //}

    //public static float GetExpirementDPI()
    //{
    //    //float difference = Screen.dpi - GetPhoneDPI();

    //    //float difference = GetPhoneDPI() - Screen.dpi;

    //    //float totalDPI = GetPhoneDPI() - difference;

    //    //float difference = Screen.dpi - GetPhoneDPI();

    //    //float difference = GetPhoneDPI() - Screen.dpi;

    //    float totalDPI = difference - GetPhoneDPI();

    //    return totalDPI;
    //}
}
