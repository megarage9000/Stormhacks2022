using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_MouseLock : MonoBehaviour
{
    private bool pauseToggle;
    private DefaultInput defaultInput;
    public GameObject pauseMenu;

    private void Awake()
    {
        defaultInput = new DefaultInput();
        defaultInput.Enable();
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        
    }

    void Update()
    {
        //Hides the cursor if escape key is pressed, also show pause menu

        if (defaultInput.Character.Menu.WasPressedThisFrame())
        {
            if (pauseToggle)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

            pauseToggle = !pauseToggle;
        }
    }

    public void PauseGame()
    {
 /*       Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;*/
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
/*        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/
        pauseMenu.SetActive(false);
        pauseToggle = !pauseToggle;
    }

}
