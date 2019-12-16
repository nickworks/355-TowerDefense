using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{
    /// <summary>
    /// this does a lot
    /// sets the towers that we spawn from this script to a grid and sets viusal to mouse
    /// sets what tower to spawn
    /// sets what tower u are clicked on
    /// 
    /// </summary>
    public class ClickToSpawnTower : MonoBehaviour
    {

        public bool buildDarkTower = true;//whther to spawn this tower or not
        public bool buildNatrueTower = false;//whther to spawn this tower or not
        public bool buildLightTower = false;//whther to spawn this tower or not

        /// <summary>
        /// sets up the grid cords
        /// </summary>
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
        public Vector2 gridOffset = Vector2.zero; // moves the grid
        public int towerCols = 4;// whoe many cols
        public int towerRows = 4;// how may rows
        public float gridSize = 1.25f;// how big the grid spots are
        public Transform gridHelper;// oves the visual where 
        public LayerMask objectsThatSupportTowers; // sets a layer masj so only objects checkon that layer will be affectd 
        public LayerMask clickableObjects;// sets a layer masj so only objects checkon that layer will be affectd 
        Tower[,] towers;//sets a towers array
       
        Camera cam; // refrence to camera
        public Tower towerPrefab; // brings in tower to spawn 
        public Tower towerPrefabLight;// brings in tower to spawn
        public Tower towerPrefabNature;// brings in tower to spawn

        private float gridX;
        static Tower _currentlySelectedTower; // checks what tower is selected
      /// <summary>
      /// stes what tower is selected
      /// </summary>
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
        /// <summary>
        /// sets refrence of cam and sets up array for tower cols and towerrols
        /// </summary>
        void Start()
        {
            cam = GetComponent<Camera>();
            towers = new Tower[towerCols, towerRows];
        }

        // Update is called once per frame
        /// <summary>
        /// moves the visual of where towers are being place
        /// spawns towers on grids 
        /// allows towers to be clicked
        /// </summary>
        void Update()
        {
            SetHelperToMouse();
            SpawnTowerOnRightClick();
            ClickedTower();
        }
        /// <summary>
        /// checks what towers are cliceked
        /// and cast a ray to click a towere 
        /// </summary>
       public void ClickedTower()
        {
            if (Input.GetButtonDown("Fire1"))
            { // on left click:
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, through the mouse

                if (Physics.Raycast(ray, out RaycastHit hit, 50, clickableObjects))
                { // shoot ray into scene, detect hit

                    Tower tower = hit.collider.GetComponentInParent<Tower>();
                    if (tower != null)
                    {
                        
                        currentlySelectedTower = tower;
                   
                    }



                }
            }
        }
        /// <summary>
        /// spawns a tower on the grid on right click acording to whatever tower is selcted
        /// </summary>
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
                        if (buildLightTower == true)
                        {
                            Tower tower = Instantiate(towerPrefabLight, gridHelper.position, Quaternion.identity);

                            towers[grid.x, grid.y] = tower;
                        }
                        if (buildNatrueTower == true)
                        {
                            Tower tower = Instantiate(towerPrefabNature, gridHelper.position, Quaternion.identity);

                            towers[grid.x, grid.y] = tower;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// mkaes so this tower is spawned and no others
        /// </summary>
        public void SpawnDarkTower()
        {
            buildDarkTower = true;
            buildNatrueTower = false;
            buildLightTower = false;
        }
        /// <summary>
        /// mkaes so this tower is spawned and no others
        /// </summary>
        public void SpawnLightTower()
        {
            buildDarkTower = false;
            buildNatrueTower = false;
            buildLightTower = true;
        }
        /// <summary>
        /// mkaes so this tower is spawned and no others
        /// </summary>
        public void SpawnNatrueTower()
        {
            buildDarkTower = false;
            buildNatrueTower = true;
            buildLightTower = false;
        }
        /// <summary>
        /// checks if where ur clicking is on vaild grid cords
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private bool IsValidGridCoords(GridCoords grid)
        {
             if (grid.x < 0) return false;
            if (grid.y < 0) return false;
            if (grid.x >= towers.GetLength(0)) return false;
            if (grid.y >= towers.GetLength(1)) return false;


            return true;
        }
        /// <summary>
        /// checks  if towers on vaild cords
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private Tower LookUpTower(GridCoords grid)
        {
            if (!IsValidGridCoords(grid)) return null;
            return towers[grid.x, grid.y];
        }
        /// <summary>
        /// makes the visual fllow the mouse so u can see where tower spanes
        /// </summary>

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
        /// <returns></returns
        /// >
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
