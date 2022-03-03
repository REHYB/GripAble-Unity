using System;
using UnityEngine;

namespace Gripable
{
    public class ClientGateway
    {
        private AndroidJavaObject _javaClientGateway;

        public ClientGateway(AndroidJavaObject context)
        {
            _javaClientGateway = new AndroidJavaObject("com.gripable.gripablebleandroid.unity.ClientGatewayUnity", context);
        }

        public DeviceGateway GetDeviceGateway(string macAddress)
        {
            AndroidJavaObject javaDeviceGateway = _javaClientGateway.Call<AndroidJavaObject>("getDeviceGateway", macAddress);
            return new DeviceGateway(javaDeviceGateway);
        }
    }
}
