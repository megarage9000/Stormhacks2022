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

    void Awake()
    {
        JoinHostButton.onClick.AddListener(async () =>
        {
            string code = JoinCodeInput.text;
            bool res = await NetworkHelpers.JoinHost(code);
            if (res)
            {
                LoadPlayScene();
            }
        });

        StartHostButton.onClick.AddListener(async () =>
        {
            bool res = await NetworkHelpers.StartHost();
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
        Destroy(GameObject.FindGameObjectWithTag("SessionJoinUI"));
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
