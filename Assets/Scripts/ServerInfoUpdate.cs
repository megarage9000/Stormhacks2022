using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerInfoUpdate : MonoBehaviour
{
    public Text serverInfo;
    void Update()
    {
        serverInfo.text = NetworkHelpers.GetServerInfo();
    }
}
