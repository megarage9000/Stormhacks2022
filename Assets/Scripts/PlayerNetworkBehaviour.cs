using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetworkBehaviour : NetworkBehaviour
{
    public Camera playerCamera;
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        // Destroy the camera if it isn't the owner
        if(!IsOwner && playerCamera) {
            Destroy(playerCamera);
        }
    }
}
