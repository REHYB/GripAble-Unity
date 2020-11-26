using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gripable
{
    public class AndroidHelper 
    {
        private static AndroidJavaClass _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        private static AndroidJavaClass _intentClass = new AndroidJavaClass("android.content.Intent");

        public static AndroidJavaObject GetMainActivity()
        {
            return _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }

        public static byte[] GetExtraByteArrayDataFromLaunchIntent(string key)
        {
            AndroidJavaObject intent = GetAndroidIntent();
            return intent.Call<byte[]>("getByteArrayExtra", key);
        }

        private static AndroidJavaObject GetAndroidIntent()
        {
            var currentActivity = GetMainActivity();
            return currentActivity.Call<AndroidJavaObject>("getIntent");
        }

        public static AndroidJavaObject GetApplicationContext()
        {
            AndroidJavaObject mainActivity = GetMainActivity();
            if (mainActivity == null)
            {
                return null;
            }
            return mainActivity.Call<AndroidJavaObject>("getApplicationContext");
        }

        public static bool LaunchApp(string packageName)
        {
            AndroidJavaObject currentActivity = GetMainActivity();
            var packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            var launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", packageName);

            if (launchIntent != null)
            {
                launchIntent.Call<AndroidJavaObject>("putExtra", "unity_package_name", Application.identifier);
                launchIntent.Call<AndroidJavaObject>("addFlags", _intentClass.GetStatic<int>("FLAG_ACTIVITY_NO_ANIMATION"));

                //Debug.Log("Launching Package name: " + packageName);
                //currentActivity.Call("startActivity", launchIntent);

                //Note: override default finish() animation to app at top-of-stack Recents
                currentActivity.Call("overridePendingTransition", 0, 0);

                currentActivity.Call("finish");
                HideCurrentActivityFromRecents(true);
                return true;
            }

            Debug.LogError("Could not get launch intent from PackageManager for " + packageName);
            return false;
        }

        public static void HideCurrentActivityFromRecents(bool hide)
        {
            AndroidJavaObject currentActivity = GetMainActivity();
            var applicationManager = currentActivity.Call<AndroidJavaObject>("getSystemService", "activity");
            if (applicationManager != null)
            {
                var appTasksObj = applicationManager.Call<AndroidJavaObject>("getAppTasks");
                if (appTasksObj != null && appTasksObj.Call<int>("size") > 0)
                {
                    appTasksObj.Call<AndroidJavaObject>("get", 0).Call("setExcludeFromRecents", hide);
                }
            }
        }

    }
}
