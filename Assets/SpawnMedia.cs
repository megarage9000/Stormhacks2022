using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnMedia : MonoBehaviour
{
    public GameObject media;
    // Start is called before the first frame update
    void Start()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            GameObject obj = Instantiate(media, new Vector3(0, 0, 10), Quaternion.identity);
            obj.GetComponent<NetworkObject>().Spawn();
        }
    }
}
