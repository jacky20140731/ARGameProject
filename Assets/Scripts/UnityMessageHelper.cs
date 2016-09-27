using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public class UnityMessageHelper
{
    private static UnityMessageHelper mInstance;

    public static UnityMessageHelper Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new UnityMessageHelper();
            }
            return mInstance;
        }
    }

    private AndroidJavaObject javaObj = null;

    private UnityMessageHelper()
    {
        init();
    }

    public void init()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		javaObj = jc.GetStatic<AndroidJavaObject>("currentActivity");
#endif

    }

    public void loadBaseMap()
    {
        javaObj.Call("loadBaseMap");
    }
}
