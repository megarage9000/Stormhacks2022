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
        Debug.Log("Clearing UI");
        JoinHostButton.gameObject.SetActive(false);
        StartHostButton.gameObject.SetActive(false);
        JoinCodeInput.gameObject.SetActive(false);
        Destroy(InitCamera);
    }

    void DisplayServerInfo()
    {
        var displayStr = manager.GetServerInfo();
        ServerInfo.text = displayStr;
    }

    private void Update()
    {
        DisplayServerInfo();
    }
}
