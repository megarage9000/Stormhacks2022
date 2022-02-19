using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class HelloWorldPlayerNetworking : NetworkBehaviour
{
    public NetworkVariable<Vector3> position = new NetworkVariable<Vector3>();

    public override void OnNetworkSpawn()
    {
        if(IsOwner) {
            Move();
        }
    }
    
    public void Move() {
        // If the server is owned by the player, we can simply the player
        if(NetworkManager.Singleton.IsServer) {
            var randomPosition = GetRandomPositionOnPlane();
            transform.position = randomPosition;
            position.Value = randomPosition;
        }
        // Send a rpc command on the server to move the client 
        else{
            SubmitPositionRequestServerRpc();
        }
    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default) {
        position.Value = GetRandomPositionOnPlane();
    }
    static Vector3 GetRandomPositionOnPlane(){
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

    void Update() {
        transform.position = position.Value;
    }

}

