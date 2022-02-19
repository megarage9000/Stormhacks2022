using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class scr_Models
{
    #region - Player -

    [Serializable]
    public class PlayerSettignsModel
     {
        [Header("View Settings")]
        public float ViewXSensitivity;
        public float ViewYSensitivity;

        public bool ViewXInverted;
        public bool ViewYInverted;
    }

    #endregion
}
