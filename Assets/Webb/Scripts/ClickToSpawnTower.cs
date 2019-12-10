using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{
    public class ClickToSpawnTower : MonoBehaviour
    {
        public bool buildDarkTower;
        struct GridCoords
        {
            public int x;
            public int y;
           
                public GridCoords(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            
        }
        public Vector2 gridOffset = Vector2.zero;
        public int towerCols = 4;
        public int towerRows = 4;
        public float gridSize = 1.25f;
        public Transform gridHelper;
        public LayerMask objectsThatSupportTowers;
        public LayerMask clickableObjects;
        Tower[,] towers;
       
        Camera cam;
        public Tower towerPrefab;
               
        private float gridX;
        static Tower _currentlySelectedTower;
         static public Tower currentlySelectedTower
        {
            get { return _currentlySelectedTower; }
            set
            {
                if (_currentlySelectedTower != null) _currentlySelectedTower.EndSelect();
                _currentlySelectedTower = value;
                if (_currentlySelectedTower != null) _currentlySelectedTower.StartSelect();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();
            towers = new Tower[towerCols, towerRows];
        }

        // Update is called once per frame
        void Update()
        {
            SetHelperToMouse();
            SpawnTowerOnRightClick();
            ClickedTower();
        }

       public void ClickedTower()
        {
            if (Input.GetButtonDown("Fire1"))
            { // on left click:
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, through the mouse

                if (Physics.Raycast(ray, out RaycastHit hit, 50, clickableObjects))
                { // shoot ray into scene, detect hit

                    Tower tower = hit.collider.GetComponent<Tower>();
                    if (tower != null)
                    {
                        
                        currentlySelectedTower = tower;
                   
                    }



                }
            }
        }

        private void SpawnTowerOnRightClick()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                GridCoords grid = CoordsWorldToGrid(gridHelper.position);
                if (IsValidGridCoords(grid))
                {
                    Tower exstingTower = LookUpTower(grid);
                    if (exstingTower == null)
                    {
                        if (buildDarkTower == true)
                        {
                            Tower tower = Instantiate(towerPrefab, gridHelper.position, Quaternion.identity);

                            towers[grid.x, grid.y] = tower;
                        }
                    }
                }
            }
        }
        public void SpawnDarkTower()
        {
            buildDarkTower = true;
        }
        private bool IsValidGridCoords(GridCoords grid)
        {
             if (grid.x < 0) return false;
            if (grid.y < 0) return false;
            if (grid.x >= towers.GetLength(0)) return false;
            if (grid.y >= towers.GetLength(1)) return false;


            return true;
        }
        private Tower LookUpTower(GridCoords grid)
        {
            if (!IsValidGridCoords(grid)) return null;
            return towers[grid.x, grid.y];
        }

        private void SetHelperToMouse()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // creat a ray from the camera through
            if (Physics.Raycast(ray, out RaycastHit hit, 50, objectsThatSupportTowers))
            { // 



                GridCoords gridPos = CoordsWorldToGrid(hit.point);
                bool isVaildPos = IsValidGridCoords(gridPos);
                gridHelper.gameObject.SetActive(isVaildPos);
                Vector3 worldPos = CoordsGridToWorld(gridPos);
                worldPos.y = hit.point.y + +0.01f;
                gridHelper.position = worldPos;
                // tell new agent to go where we clicked

            }
        }
        /// <summary>
        /// returns the world postions ceter of provide gridcors
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private Vector3 CoordsGridToWorld(GridCoords grid)
        {
            float x = grid.x * gridSize + gridSize / 2 + gridOffset.x;
            float y = grid.y * gridSize + gridSize / 2 + gridOffset.y;
            return new Vector3(x, 0,  y);
        }

        private GridCoords CoordsWorldToGrid(Vector3 worldPos)
        {
            int gridX = Mathf.FloorToInt((worldPos.x - gridOffset.x) / gridSize);
            int gridY = Mathf.FloorToInt((worldPos.z - gridOffset.y) / gridSize);
            return new GridCoords(gridX, gridY);
        }
    }
}
