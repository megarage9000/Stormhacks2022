using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_MouseLock : MonoBehaviour
{
    private bool pauseToggle;
    private DefaultInput defaultInput;

    private void Awake()
    {
        defaultInput = new DefaultInput();
        defaultInput.Enable();
    }

    void Update()
    {
        //Hides the cursor if escape key is pressed, also show pause menu

        if (defaultInput.Character.Menu.WasPressedThisFrame())
        {
            if (pauseToggle)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Debug.Log("Menu Off / Cursor Locked");
            }
            else
            {
                Debug.Log("Menu On / Cursor Unlocked");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            pauseToggle = !pauseToggle;
        }
    }
}
