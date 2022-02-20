using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class HelloWorldNetworkManager : MonoBehaviour
{
    public Camera initCamera;

    public void DestroyCamera() {
        initCamera.enabled = false;
    }
    void OnGUI() {
        GUILayout.BeginArea(new Rect(100, 100, 300, 300));
        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer) {
            StartButtons();
        }
        else {
            StatusLabels();
            DestroyCamera(); 
        }

        GUILayout.EndArea();
    }

    
    async void StartButtons() {
        if(GUILayout.Button("Host")) {

            if(RelayManager.Instance.IsRelayEnabled)
            {
                await RelayManager.Instance.SetupRelay();
            }
            if(NetworkManager.Singleton.StartHost()){
                Debug.Log("Starting the Host...");
            }
            else {
                Debug.Log("Unable to start the Host");
            }
        } 
            
        if(GUILayout.Button("Client")){
            if (RelayManager.Instance.IsRelayEnabled)
            {
                //await RelayManager.Instance.JoinRelay();
            }
            if (NetworkManager.Singleton.StartClient()){
                Debug.Log("Starting the Client...");
            }
            else {
                Debug.Log("Unable to start the Client");
            }
        }
        if(GUILayout.Button("Server")){
            if(NetworkManager.Singleton.StartServer()){
                Debug.Log("Starting the Server...");
            }
            else {
                Debug.Log("Unable to start the Server");
            }
        }
    }

    static void StatusLabels() {
        var mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }
}
