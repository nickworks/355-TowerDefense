using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class ClickToSpawnTower : MonoBehaviour
    {
        public GameObject enemyPrefab;
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
            if (Input.GetButtonDown("Fire1")) // on click:
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, throught the mouse position

                if (Physics.Raycast(ray, out RaycastHit hit)) // shoot ray into scene, detect hit
                {

                    Instantiate(enemyPrefab, hit.point, Quaternion.identity);

                }


            }

            if (Input.GetButtonDown("Fire2")) // on click:
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, throught the mouse position

                if (Physics.Raycast(ray, out RaycastHit hit)) // shoot ray into scene, detect hit
                {

                    Instantiate(towerPrefab, hit.point, Quaternion.identity);

                }


            }



        }
    }
}