using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// this class is to insure that the health bar of anything always faces the camera
    /// </summary>
    public class FixGUIRotation : MonoBehaviour
    {

        public Transform gui; // get the tranform of the desired GUI

        // LateUpdate is called after all Update functions have been called
        void LateUpdate()
        {
            gui.rotation = Quaternion.Euler(60, 0, 0); // this rotates the GUI towards the camera
        } // end update
    } // end class
} // end namespace