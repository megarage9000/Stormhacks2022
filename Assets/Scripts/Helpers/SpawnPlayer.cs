using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject Player;

    private void Awake()
    {
        Player.GetComponent<NetworkObject>().Spawn(true);
        Destroy(this);
    }
}
