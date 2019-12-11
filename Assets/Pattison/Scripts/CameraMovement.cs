using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison {
    public class CameraMovement : MonoBehaviour {


        public float mouseSensitivity = 1;
        public float easing = 5;

        Vector3 targetPosition = Vector3.zero;

        bool isDragging = false;

        void Start() {
            targetPosition = transform.position;
        }

        // Update is called once per frame
        void Update() {

            if(Input.GetButtonDown("Fire1")) isDragging = true; // left mouse button goes down
            if(Input.GetButtonUp("Fire1")) isDragging = false; // left mouse button goes up


            if (isDragging) {
                float mx = Input.GetAxis("Mouse X");
                float my = Input.GetAxis("Mouse Y");

                targetPosition -= new Vector3(mx, 0, my) * mouseSensitivity;

            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * easing);


        }
    }
}