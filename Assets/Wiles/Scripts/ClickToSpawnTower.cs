using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class ClickToSpawnTower : MonoBehaviour
    {

        struct GridCoords
        {
            public int x;
            public int y;
            public GridCoords(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            /*public GridCoorda(Vector3 v)
            {
                x = (int)v.x;
                y = (int)v.y;
            }*/
        }


        public int towerCols = 4;
        public int towerRows = 4;

        public float gridSize = 1.5f;
        public Vector2 gridOffset = Vector2.zero;

        public Transform gridHelper;

        public GameObject enemyPrefab;
        public Tower towerPrefab;
        public LayerMask objectsThatSupportTowers;
        public LayerMask clickableObjects;

        Tower _currentlySelectedTower;
        public Tower currentlySelectedTower
        {
            get { return _currentlySelectedTower; }
            set {
                if (_currentlySelectedTower != null) _currentlySelectedTower.EndSelect();
                _currentlySelectedTower = value;
                if (_currentlySelectedTower != null) _currentlySelectedTower.StartSelect();
            }
        }

        Tower _currentTowerToBuy;
        public Tower currentTowerToBuy
        {
            get { return _currentTowerToBuy; }
            set
            {
                //if (_currentTowerToBuy != null) _currentTowerToBuy.EndSelect();
                _currentTowerToBuy = value;
                //if (_currentTowerToBuy != null) _currentTowerToBuy.StartSelect();
            }
        }


        Tower[,] towers;

        Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();

            towers = new Tower[towerCols, towerRows];

        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (Input.GetButtonDown("Fire1")) // on click:
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, throught the mouse position

                if (Physics.Raycast(ray, out RaycastHit hit)) // shoot ray into scene, detect hit
                {

                    Instantiate(enemyPrefab, hit.point, Quaternion.identity);

                }


            }
            */
            SetHelperToMouse();
            SpawnTowerOnRightClick();

            if (Input.GetButtonDown("Fire1")) // on left click:
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, throught the mouse position

                if (Physics.Raycast(ray, out RaycastHit hit, 50, objectsThatSupportTowers)) // shoot ray into scene, detect hit
                {

                    Tower tower = hit.collider.GetComponent<Tower>();
                    if(tower != null)
                    {
                        //if (currentlySelectedTower != null) currentlySelectedTower.EndSelect();
                        currentlySelectedTower = tower;
                        //currentlySelectedTower.StartSelect();
                    }



                }
            }
        }

        private void SpawnTowerOnRightClick()
        {
            if (Input.GetButtonDown("Fire2")) // on click:
            {

                GridCoords grid = CoordsWorldToGrid(gridHelper.position);


                if (IsValidGridCoords(grid))
                {

                    Tower existingTower = LookupTower(grid);

                    if (existingTower == null)
                    {

                        // CHECK AI NAVIGATION PATHS
                        if (currentTowerToBuy != null)
                        {
                            Tower tower = Instantiate(currentTowerToBuy, gridHelper.position, Quaternion.identity);
                            towers[grid.x, grid.y] = tower;
                        }
                    }
                }


            }
        }

        private Tower LookupTower(GridCoords grid)
        {
            if (!IsValidGridCoords(grid)) return null;

            return towers[grid.x, grid.y];
        }

        private bool IsValidGridCoords(GridCoords grid)
        {
            if (grid.x < 0) return false;
            if (grid.y < 0) return false;
            if (grid.x >= towers.GetLength(0)) return false;
            if (grid.y >= towers.GetLength(1)) return false;

            return true;
        }

        private void SetHelperToMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, throught the mouse position

            if (Physics.Raycast(ray, out RaycastHit hit, 50, objectsThatSupportTowers)) // shoot ray into scene, detect hit
            {
                GridCoords gridPos = CoordsWorldToGrid(hit.point);

                bool isValidPos = IsValidGridCoords(gridPos);

                gridHelper.gameObject.SetActive(isValidPos);


                Vector3 worldPos = CoordsGridToWorld(gridPos);
                worldPos.y = hit.point.y + .01f;

                gridHelper.position = worldPos;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns>The world-position CENTER of the provided GridCoords.</returns>
        private Vector3 CoordsGridToWorld(GridCoords grid)
        {
            float x = grid.x * gridSize + gridSize / 2 + gridOffset.x;
            float y = grid.y * gridSize + gridSize / 2 + gridOffset.y;

            return new Vector3(x, 0, y);
        }

        private GridCoords CoordsWorldToGrid(Vector3 worldPos)
        {
            int gridX = Mathf.FloorToInt((worldPos.x - gridOffset.x) / gridSize);
            int gridY = Mathf.FloorToInt((worldPos.z - gridOffset.y) / gridSize);

            return new GridCoords(gridX, gridY);
        }
    }
}