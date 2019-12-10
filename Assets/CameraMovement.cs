using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    bool  isDraging;
    public float mouseSensitivity = 1;
    public float easing = 5;
    Vector3 targetPostion =  Vector3.zero;
  
    void Start()
    {
        targetPostion = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) isDraging = true;
        if (Input.GetButtonUp("Fire1")) isDraging = false;
        if (isDraging)
        {
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");
            targetPostion -= new Vector3(mx, 0, my) * mouseSensitivity;
        }
        transform.position =  Vector3.Lerp(transform.position , targetPostion,Time.deltaTime * easing) * mouseSensitivity;
    }
}
