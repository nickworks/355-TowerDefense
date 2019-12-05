using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace White
{
    public class ClickToSpawnTower : MonoBehaviour
    {
        public GameObject towerPrefab;
        Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                // on click:
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, to the mouse

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Instantiate(towerPrefab, hit.point, Quaternion.identity);

                }
            }
        }
    }
}