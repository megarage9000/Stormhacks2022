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
    public Camera InitCamera;
    public Canvas canvas;

    private bool isHost = true;


    void Awake()
    {
        JoinHostButton.onClick.AddListener(async () =>
        {
            string code = JoinCodeInput.text;
            bool res = await NetworkHelpers.JoinRelay(code);
            if (res)
            {
                isHost = false;
                LoadPlayScene();
            }
        });

        StartHostButton.onClick.AddListener(async () =>
        {
            bool res = await NetworkHelpers.StartRelay();
            if (res)
            {
                isHost = true;
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
        SceneComm sceneComm = new SceneComm();
        sceneComm.setToHost(isHost);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
