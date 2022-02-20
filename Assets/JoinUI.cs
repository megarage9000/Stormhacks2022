using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class JoinUI : MonoBehaviour
{

    public InputField JoinCodeInput;
    public Button JoinHostButton;
    public Button StartHostButton;
    public Text ServerInfo;
    public NetworkHelpers manager;
    public Camera InitCamera;

    void Awake()
    {
        JoinHostButton.onClick.AddListener(async () =>
        {
            string code = JoinCodeInput.text;
            bool res = await manager.JoinHost(code);
            if (res)
            {
                ClearUIMenu();
                DisplayServerInfo();
            }
        });

        StartHostButton.onClick.AddListener(async () =>
        {
            bool res = await manager.StartHost();
            if (res)
            {
                ClearUIMenu();
                DisplayServerInfo();
            }
        });
    }

    void ClearUIMenu()
    {
        Destroy(JoinHostButton);
        Destroy(StartHostButton);
        Destroy(JoinCodeInput);
        Destroy(InitCamera);
    }

    void DisplayServerInfo()
    {
        var displayStr = manager.GetServerInfo();
        ServerInfo.text = displayStr;
    }
}
