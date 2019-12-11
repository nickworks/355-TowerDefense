using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison {
    public class FixGUIRotation : MonoBehaviour {


        public Transform gui;
        
        void LateUpdate() {
            gui.rotation = Quaternion.Euler(60, 0, 0);
        }
    }
}