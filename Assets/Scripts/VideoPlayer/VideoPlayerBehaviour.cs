using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class VideoPlayerBehaviour : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsClient && IsServer)
        {
            gameObject.AddComponent<VideoPlayerHost>();
        }
        else if (IsClient)
        {
            gameObject.AddComponent<VideoPlayerClient>();
        }
    }
}
