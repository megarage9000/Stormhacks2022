using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class JoinSession : MonoBehaviour
{
    public InputField JoinCodeInput;
    public Button JoinHostButton;
    public Button StartHostButton;
    public NetworkHelpers manager;
    public Camera InitCamera;
    public Canvas canvas;

    void Awake()
    {
        JoinHostButton.onClick.AddListener(async () =>
        {
            string code = JoinCodeInput.text;
            bool res = await manager.JoinHost(code);
            if (res)
            {
                LoadPlayScene();
            }
        });

        StartHostButton.onClick.AddListener(async () =>
        {
            bool res = await manager.StartHost();
            if (res)
            {
                LoadPlayScene();
            }
        });
    }

    void LoadPlayScene()
    {
        Debug.Log("Clearing UI");
        JoinHostButton.gameObject.SetActive(false);
        StartHostButton.gameObject.SetActive(false);
        JoinCodeInput.gameObject.SetActive(false);
        Destroy(InitCamera);
        Destroy(canvas);
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
    }
}
