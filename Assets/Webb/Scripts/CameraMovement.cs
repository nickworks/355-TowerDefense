using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    bool  isDraging;// set to see if you are tying to move camera
    public float mouseSensitivity = 1;// how fast mouse moves camera
    public float easing = 5;// how fast camera slows down
    Vector3 targetPostion =  Vector3.zero;// going to be mouse postion
  /// <summary>
  /// sets the postion of targetpostion
  /// </summary>
    void Start()
    {
        targetPostion = transform.position;
        
    }
    /// <summary>
    /// changes the camera based on wether or not the camera is being moved if it is updaets with mouse postion.
    /// </summary>
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
