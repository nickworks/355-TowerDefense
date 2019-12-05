using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pattison
{

    public class ClickToSpawnTower : MonoBehaviour
    {

        struct GridCoords
        {
            public int x;
            public int y;
            public GridCoords(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }


        public int towerCols = 4;
        public int towerRows = 4;

        public float gridSize = 1.5f;
        public Vector2 gridOffset = Vector2.zero;

        public Transform gridHelper;


        public Tower towerPrefab;
        public LayerMask objectsThatSupportTowers;
        public LayerMask clickableObjects;

        Tower _currentlySelectedTower;

        public Tower currentlySelectedTower {
            get { return _currentlySelectedTower; }
            set {
                if (_currentlySelectedTower != null) _currentlySelectedTower.EndSelect();
                _currentlySelectedTower = value;
                if (_currentlySelectedTower != null) _currentlySelectedTower.StartSelect();
            }
        }

        Tower[,] towers;

        Camera cam;

        void Start() {
            cam = GetComponent<Camera>();

            towers = new Tower[towerCols, towerRows];

        }

        // Update is called once per frame
        void Update() {

            SetHelperToMouse();
            SpawnTowerOnRightClick();

            if (Input.GetButtonDown("Fire1")) { // on left click:
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, through the mouse

                if (Physics.Raycast(ray, out RaycastHit hit, 50, clickableObjects)) { // shoot ray into scene, detect hit

                    Tower tower = hit.collider.GetComponent<Tower>();
                    if(tower != null) {
                        currentlySelectedTower = tower;
                    }



                }

            }
        }

        private void SpawnTowerOnRightClick() {
            if (Input.GetButtonDown("Fire2")) { // on right click:

                GridCoords grid = CoordsWorldToGrid(gridHelper.position);


                if (IsValidGridCoords(grid)) {

                    Tower existingTower = LookupTower(grid);

                    if (existingTower == null) {

                        // CHECK AI NAVIGATION PATHS

                        Tower tower = Instantiate(towerPrefab, gridHelper.position, Quaternion.identity);
                        towers[grid.x, grid.y] = tower;
                    }
                }
            }
        }

        private Tower LookupTower(GridCoords grid) {

            if (!IsValidGridCoords(grid)) return null;

            return towers[grid.x, grid.y];
        }

        private bool IsValidGridCoords(GridCoords grid) {

            if (grid.x < 0) return false;
            if (grid.y < 0) return false;
            if (grid.x >= towers.GetLength(0)) return false;
            if (grid.y >= towers.GetLength(1)) return false;

            return true;
        }

        private void SetHelperToMouse() {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, through the mouse

            if (Physics.Raycast(ray, out RaycastHit hit, 50, objectsThatSupportTowers)) { // shoot ray into scene, detect hit


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
        private Vector3 CoordsGridToWorld(GridCoords grid) {
            float x = grid.x * gridSize + gridSize / 2 + gridOffset.x;
            float y = grid.y * gridSize + gridSize / 2 + gridOffset.y;

            return new Vector3(x, 0, y);
        }

        private GridCoords CoordsWorldToGrid(Vector3 worldPos) {
            int gridX = Mathf.FloorToInt((worldPos.x - gridOffset.x) / gridSize);
            int gridY = Mathf.FloorToInt((worldPos.z - gridOffset.y) / gridSize);

            return new GridCoords(gridX, gridY);
        }
    }
}