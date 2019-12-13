using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class CameraMovement : MonoBehaviour
    {

        public float mouseSensitivity = 1;
        public float easing = 5;

        Vector3 targetPosition;

        bool isDragging = false;
        void Start()
        {
            targetPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
           
            if (Input.GetButtonDown("Fire1"))
            {
                isDragging = true; // mouse button goes down
            }
            if (Input.GetButtonUp("Fire1"))
            {
                isDragging = false; // mouse button goes down
            }

            if (isDragging)
            {
                float mx = Input.GetAxis("Mouse X");
                float my = Input.GetAxis("Mouse Y");

                targetPosition -= new Vector3(mx, 0, my) * mouseSensitivity;
                transform.position = Vector3.Lerp(transform.position, targetPosition, (Time.deltaTime * easing));
            }
                       
        }
    }
} 