using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace White
{
    /// <summary>
    /// This class handles the spawning of towers.
    /// </summary>
    public class ClickToSpawnTower : MonoBehaviour
    {
        /// <summary>
        /// This struct sets up the grid coordinates.
        /// </summary>
        struct GridCoords
        {
            /// <summary>
            /// The x value.
            /// </summary>
            public int x;

            /// <summary>
            /// The y value.
            /// </summary>
            public int y;

            /// <summary>
            /// This function sets up the grid coordinates.
            /// </summary>
            /// <param name="x">The x value.</param>
            /// <param name="y">The y value.</param>
            public GridCoords(int x, int y)
            {
                this.x = x;
                this.y = y;
            } // ends the GridCoords() function
        } // ends the GridCoords struct

        /// <summary>
        /// The number of columns in the grid.
        /// </summary>
        public int towerCols = 4;

        /// <summary>
        /// The number of rows in the grid.
        /// </summary>
        public int towerRows = 4;

        /// <summary>
        /// The size of the grid.
        /// </summary>
        public float gridSize = 1.5f;

        /// <summary>
        /// Offsets the grid.
        /// </summary>
        public Vector2 gridOffset = Vector2.zero;

        /// <summary>
        /// The visual element that shows where towers can be placed.
        /// </summary>
        public Transform gridHelper;

        /// <summary>
        /// The tower that will be spawned.
        /// </summary>
        public Tower towerPrefab;

        /// <summary>
        /// The layer that towers can be spawned on.
        /// </summary>
        public LayerMask objectsThatSupportTowers;

        /// <summary>
        /// The array of towers.
        /// </summary>
        Tower[,] towers;

        /// <summary>
        /// The camera.
        /// </summary>
        Camera cam;

        /// <summary>
        /// This function gets the camera and grid.
        /// </summary>
        void Start()
        {
            cam = GetComponent<Camera>();

            towers = new Tower[towerCols, towerRows];
        } // ends the Start() function

        /// <summary>
        /// This function allows the towers to be spawned when the mouse is clicked.
        /// </summary>
        void Update()
        {
            SetHelperToMouse();
            if (Input.GetButtonDown("Fire2"))
            {
                /// <summary>
                /// Sets the grid helper to the world coordinates.
                /// </summary>
                GridCoords grid = CoordsWorldToGrid(gridHelper.position);

                if (IsValidGridCoords(grid))
                {
                    /// <summary>
                    /// Shows whether or not there is a tower in the current space on the grid.
                    /// </summary>
                    Tower existingTower = LookupTower(grid);

                    if (existingTower == null)
                    {
                        Tower tower = Instantiate(towerPrefab, gridHelper.position, Quaternion.identity);
                        towers[grid.x, grid.y] = tower;
                    }
                }
            }
        } // ends the Update() function

        /// <summary>
        /// This function looks to see if there is already a tower in the space on the grid.
        /// </summary>
        /// <param name="grid">The grid to check.</param>
        /// <returns>Whether or not there is a tower in the space.</returns>
        private Tower LookupTower(GridCoords grid)
        {
            if (!IsValidGridCoords(grid)) return null;

            return towers[grid.x, grid.y];
        } // ends the LookupTower() function

        /// <summary>
        /// This function checks to see if the grid coordinates are valid.
        /// </summary>
        /// <param name="grid">The grid to check.</param>
        /// <returns>Whether or not the coordinates are valid.</returns>
        private bool IsValidGridCoords(GridCoords grid)
        {
            if (grid.x < 0) return false;
            if (grid.y < 0) return false;
            if (grid.x >= towers.GetLength(0)) return false;
            if (grid.y >= towers.GetLength(1)) return false;

            return true;
        } // ends the IsValidGridCoords() function

        /// <summary>
        /// This function sets the grid helper to the mouse and allows it to move with the mouse.
        /// </summary>
        private void SetHelperToMouse()
        {
            /// <summary>
            /// creates a ray from the camera to the mouse
            /// </summary>
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, to the mouse

            if (Physics.Raycast(ray, out RaycastHit hit))//, 50, objectsThatSupportTowers))
            {
                GridCoords gridPos = CoordsWorldToGrid(hit.point);

                bool isValidPos = IsValidGridCoords(gridPos);
                gridHelper.gameObject.SetActive(isValidPos);

                Vector3 worldPos = CoordsGridToWorld(gridPos);
                worldPos.y = hit.point.y + .01f;

                gridHelper.position = worldPos;

            }
        } // ends the SetHelperToMouse()

        /// <summary>
        /// This function converts the grid coordinates to world coordinates.
        /// </summary>
        /// <param name="worldPos"></param>
        /// <returns>The world-position center of the provided GridCoords.</returns>
        private Vector3 CoordsGridToWorld(GridCoords grid)
        {
            float x = grid.x * gridSize + gridOffset.x + gridSize / 2;
            float y = grid.y * gridSize + gridOffset.y + gridSize / 2;
            return new Vector3(x, 0, y);
        } // ends the CoordsGridToWorld

        /// <summary>
        /// This function converts the world coordinates to grid coordinates.
        /// </summary>
        /// <param name="worldPos"></param>
        /// <returns>The grid-position of the provided world coordinates.</returns>
        private GridCoords CoordsWorldToGrid(Vector3 worldPos)
        {
            int gridX = Mathf.FloorToInt((worldPos.x - gridOffset.x / gridSize));
            int gridY = Mathf.FloorToInt((worldPos.z - gridOffset.y / gridSize));
            return new GridCoords(gridX, gridY);
        } // ends the CoordsWorldToGrid() function
    } // ends the ClickToSpawnTower() function
} // ends the White namespace