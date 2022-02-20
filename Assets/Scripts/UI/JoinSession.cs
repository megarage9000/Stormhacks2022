using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinSession : MonoBehaviour
{
    public InputField JoinCodeInput;
    public Button JoinHostButton;
    public Button StartHostButton;
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
            }
        });

        StartHostButton.onClick.AddListener(async () =>
        {
            bool res = await manager.StartHost();
            if (res)
            {
                ClearUIMenu();
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
}
