using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class FixGUIRotation : MonoBehaviour
    {

        public Transform gui;

        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            gui.rotation = Quaternion.Euler(60, 0, 0);
        }
    }
}