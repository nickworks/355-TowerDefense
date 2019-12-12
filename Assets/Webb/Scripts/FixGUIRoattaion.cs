using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb{
    public class FixGUIRoattaion : MonoBehaviour
    {
        public Transform gui;
        public Transform guiBG;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            gui.rotation = Quaternion.Euler(60, 0, 0);
            guiBG.rotation = Quaternion.Euler(60, 0, 0);

        }
    }
}
