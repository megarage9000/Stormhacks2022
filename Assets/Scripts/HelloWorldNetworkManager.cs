using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

public class HelloWorldNetworkManager : MonoBehaviour
{
    private string JoinCode;

/*    void OnGUI() {
        GUILayout.BeginArea(new Rect(100, 100, 300, 300));
        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer) {
            // StartButtons();
        }
        else {
            StatusLabels();
            DestroyCamera(); 
        }

        GUILayout.EndArea();
    }*/

    public async Task<bool> JoinHost(string code)
    {
        if (RelayManager.Instance.IsRelayEnabled && !string.IsNullOrEmpty(code))
        {
            await RelayManager.Instance.JoinRelay(code);
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Starting the Client...");
                JoinCode = code;
                return true;
            }
            else
            {
                Debug.Log("Unable to start the Client");
                return false;
            }
        }
        return false;
    }

    public async Task<bool> StartHost()
    {
        if (RelayManager.Instance.IsRelayEnabled)
        {
            RelayHostData hostData = await RelayManager.Instance.SetupRelay();
            JoinCode = hostData.JoinCode;
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Starting the Host...");
                return true;
            }
            else
            {
                Debug.Log("Unable to start the Host");
                return false;
            }
        }
        return false;
    }
/*    static void StatusLabels() {
        var mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name +
            "Mode: " + (NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client") +
            "Join Code: " + JoinCode ); 
        GUILayout.Label("Mode: " + mode);
    }*/

    public string GetServerInfo()
    {
        return "Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name +
            "\nMode: " + (NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client" +
            "\nJoin Code: " + JoinCode);
    }
}