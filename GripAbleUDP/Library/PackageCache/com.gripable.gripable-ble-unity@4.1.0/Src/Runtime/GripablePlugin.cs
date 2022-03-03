using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gripable
{
    public sealed class GripablePlugin
    {
        private static GripablePlugin _instance;
        private static GripablePlugin Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GripablePlugin();
                }
                return _instance;
            }
        }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static GripablePlugin()
        {
            GameObject asyncDispatcher = new GameObject();
            asyncDispatcher.name = "Dispatcher";
            asyncDispatcher.AddComponent<AsyncDispatcher>();
#if !UNITY_EDITOR
            UnityEngine.Object.DontDestroyOnLoad(asyncDispatcher);
#endif
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Instance._player = new Player();
        }

        private Player _player;
        public static Player Player
        {
            get
            {
                return Instance._player;
            }
        }

        //private ActivityLauncher _activityLauncher;
        //public static ActivityLauncher Launcher
        //{
        //    get
        //    {
        //        return Instance._activityLauncher;
        //    }
        //}

    }
}