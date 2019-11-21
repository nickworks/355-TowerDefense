using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class ClickToSpawnTower : MonoBehaviour
    {
        public GameObject towerPrefab;
        Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            { // on click
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, through scene to the mouse

                if (Physics.Raycast(ray, out RaycastHit hit))
                {// shoot ray into scene, detect where it hit
                    Instantiate(towerPrefab, hit.point, Quaternion.identity);
                }

            }
        }
    }
}