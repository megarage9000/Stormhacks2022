using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_MouseLock : MonoBehaviour
{
    private bool pauseToggle;

    void Update()
    {
        //Hides the cursor if escape key is pressed, also show pause menu

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseToggle)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            pauseToggle = !pauseToggle;
        }
    }
}
