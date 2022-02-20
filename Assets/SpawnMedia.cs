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
        GameObject obj = Instantiate(media, Vector3.zero, Quaternion.identity);
        obj.GetComponent<NetworkObject>().Spawn();
    }
}
