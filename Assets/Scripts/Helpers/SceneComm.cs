using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneComm
{
    public static bool IsHost = false;

    public void setToHost(bool value)
    {
        IsHost = value;
    }

}
