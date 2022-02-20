using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

public static class NetworkHelpers
{
    private static string JoinCode;
    public static async Task<bool> JoinHost(string code)
    {
        if (RelayManager.Instance.IsRelayEnabled && !string.IsNullOrEmpty(code))
        {
            await RelayManager.Instance.JoinRelay(code);
            JoinCode = code;
        }
        if (NetworkManager.Singleton.StartClient())
        {
            Debug.Log("Starting the Client...");
            return true;
        }
        else
        {
            Debug.Log("Unable to start the Client");
            return false;
        }
    }

    public static async Task<bool> StartHost()
    {
        if (RelayManager.Instance.IsRelayEnabled)
        {
            await RelayManager.Instance.SetupRelay();
        }
        if (NetworkManager.Singleton.StartHost())
        {
            Debug.Log("Starting the Host...");
            JoinCode = RelayManager.Instance.JoinCode;
            return true;
        }
        else
        {
            Debug.Log("Unable to start the Host");
            return false;
        }
    }

    public static string GetServerInfo()
    {
        if(NetworkManager.Singleton != null)
        {
            var mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";
            var transport = NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name;
            return "Transport: " + transport +
                "\nMode: " + mode  +
                "\nJoin Code: " + JoinCode;
        }
        return "";
    }
}
