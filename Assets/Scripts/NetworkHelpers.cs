using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

public class NetworkHelpers : Singleton<NetworkHelpers>
{
    private string JoinCode;
    public async Task<bool> JoinHost(string code)
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

    public async Task<bool> StartHost()
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

    public string GetServerInfo()
    {
        return "Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name +
            "\nMode: " + (NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client" +
            "\nJoin Code: " + JoinCode);
    }
}
