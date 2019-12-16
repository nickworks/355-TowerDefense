using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb{
    /// <summary>
    /// sets roation of gui to face cameera
    /// </summary>
    public class FixGUIRoattaion : MonoBehaviour
    {
        public Transform gui;// refrence to gui
        public Transform guiBG;//refrence to gui background

      

        /// <summary>
        /// makes gui face camera
        /// </summary>
        void Update()
        {
            gui.rotation = Quaternion.Euler(60, 0, 0);
            guiBG.rotation = Quaternion.Euler(60, 0, 0);

        }
    }
}
