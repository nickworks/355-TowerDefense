using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{
    public class ClickToSpawnTower : MonoBehaviour
    {
        Camera cam;
        public GameObject towerPrefab;
        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // creat a ray from the camera through
                if (Physics.Raycast(ray, out RaycastHit hit))
                { // 
                  // tell new agent to go where we clicked
                    Instantiate(towerPrefab, hit.point, Quaternion.identity);
                }
            }
        }
    }
}
