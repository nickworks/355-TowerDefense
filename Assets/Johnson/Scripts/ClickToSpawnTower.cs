using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    public class ClickToSpawnTower : MonoBehaviour
    {
        public Button towerOneButton;
        public Button towerTwoButton;
        public Button towerThreeButton;

        bool isTowerOne = false;
        bool isTowerTwo = false;
        bool isTowerThree = false;

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

        public int towerCols = 4;
        public int towerRows = 4;
        public float gridSize = 1.5f;
        public Vector2 gridOffset = Vector2.zero;
        public Transform gridHelper;

        public TowerStateMachine towerPrefab;
        public LightningTowerStateMachine lightningTowerPrefab;
        public FreezeTowerStateMachine freezeTowerPrefab;
        public LayerMask objectsThatSupportTowers;

        public LayerMask clickableObjects;

        static TowerStateMachine _CurrentlySelectedTower;
        static LightningTowerStateMachine _CurrentlySelectedLightningTower;

        static public TowerStateMachine currentlySelectedTower {
            get { return _CurrentlySelectedTower;  }
            set
            {
                if (_CurrentlySelectedTower != null) _CurrentlySelectedTower.EndSelect();
                _CurrentlySelectedTower = value;
                if (_CurrentlySelectedTower != null) _CurrentlySelectedTower.StartSelect();
            }
        }
        static public LightningTowerStateMachine currentlySelectedLightningTower
        {
            get { return _CurrentlySelectedLightningTower; }
            set
            {
                if (_CurrentlySelectedLightningTower != null) _CurrentlySelectedLightningTower.EndSelect();
                _CurrentlySelectedLightningTower = value;
                if (_CurrentlySelectedLightningTower != null) _CurrentlySelectedLightningTower.StartSelect();
            }
        }

        TowerStateMachine[,] towers;
        LightningTowerStateMachine[,] lightningTowers;
        FreezeTowerStateMachine[,] freezeTowers;

        Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponent<Camera>();

            Button towerBtn = towerOneButton.GetComponent<Button>();
            towerBtn.onClick.AddListener(TowerOnClick);
            Button lightningBtn = towerTwoButton.GetComponent<Button>();
            lightningBtn.onClick.AddListener(LightningOnClick);
            Button freezeBtn = towerThreeButton.GetComponent<Button>();
            freezeBtn.onClick.AddListener(FreezeOnClick);

            towers = new TowerStateMachine[towerCols, towerRows];
            lightningTowers = new LightningTowerStateMachine[towerCols, towerRows];
            freezeTowers = new FreezeTowerStateMachine[towerCols, towerRows];
        }

        // Update is called once per frame
        void Update()
        {
            SetHelperToMouse();
            SpawnTowerOnRightClick();
            ClickToSelectTower();

        }

        private void ClickToSelectTower()
        {
            if (Input.GetButtonDown("Fire1") && Input.GetButton("Jump")) // on left click + spacebar
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, through scene to the mouse
                if (Physics.Raycast(ray, out RaycastHit hit, 50, clickableObjects))
                {// shoot ray into scene, detect where it hit

                    TowerStateMachine tower = hit.collider.GetComponent<TowerStateMachine>();
                    if (tower != null && isTowerOne)
                    {
                        currentlySelectedTower = tower;
                    }
                    LightningTowerStateMachine lightningTower = hit.collider.GetComponent<LightningTowerStateMachine>();
                    if (tower != null && isTowerTwo)
                    {
                        currentlySelectedLightningTower = lightningTower;
                    }

                }
                else
                {
                    currentlySelectedTower = null; // deselect
                    currentlySelectedLightningTower = null;
                }
            }
        }

        private void SpawnTowerOnRightClick()
        {
            if (Input.GetButtonDown("Fire2"))
            { // on click

                GridCoords grid = CoordsWorldToGrid(gridHelper.position);

                if (IsValidGridCoords(grid))
                {

                    TowerStateMachine existingTower = LookupTowerPoint(grid);
                    LightningTowerStateMachine existingLightningTower = LookupLightningPoint(grid);
                    FreezeTowerStateMachine existineFreezeTower = LookupFreezePoint(grid);

                    if (existingTower == null && existingLightningTower == null && existineFreezeTower == null)
                    {

                        // check ai nav paths
                        if (isTowerOne)
                        {
                            TowerStateMachine tower = Instantiate(towerPrefab, gridHelper.position, Quaternion.identity);
                            towers[grid.x, grid.y] = tower;
                        }
                        else if (isTowerTwo)
                        {
                            LightningTowerStateMachine lightningTower = Instantiate(lightningTowerPrefab, gridHelper.position, Quaternion.identity);
                            lightningTowers[grid.x, grid.y] = lightningTower;
                        }
                        else if (isTowerThree)
                        {
                            FreezeTowerStateMachine freezeTower = Instantiate(freezeTowerPrefab, gridHelper.position, Quaternion.identity);
                            freezeTowers[grid.x, grid.y] = freezeTower;
                        }
                    }
                }
            }
        }

        private TowerStateMachine LookupTowerPoint(GridCoords grid)
        {
            if (!IsValidGridCoords(grid)) return null;

            return towers[grid.x, grid.y];
        }

        private LightningTowerStateMachine LookupLightningPoint(GridCoords grid)
        {
            if (!IsValidGridCoords(grid)) return null;

            return lightningTowers[grid.x, grid.y];
        }

        private FreezeTowerStateMachine LookupFreezePoint(GridCoords grid)
        {
            if (!IsValidGridCoords(grid)) return null;

            return freezeTowers[grid.x, grid.y];
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
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // create a ray from the camera, through scene to the mouse

            Debug.DrawRay(ray.origin, ray.direction * 100);
            if (Physics.Raycast(ray, out RaycastHit hit, 200, objectsThatSupportTowers))
            {// shoot ray into scene, detect where it hit

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
        /// <returns>The world-position center of the provided GridCoords</returns>
        private Vector3 CoordsGridToWorld(GridCoords grid)
        {
            float x = grid.x * gridSize + gridSize / 2 + gridOffset.x;
            float y = grid.y * gridSize + gridSize / 2 + gridOffset.y;

            return new Vector3(x, 0, y);


        }

        private GridCoords CoordsWorldToGrid(Vector3 worldPos)
        {
            int gridX = Mathf.FloorToInt(worldPos.x - gridOffset.x / gridSize);
            int gridY = Mathf.FloorToInt(worldPos.z - gridOffset.y / gridSize);

            return new GridCoords(gridX, gridY);
        }
        
        void TowerOnClick()
        {
            isTowerOne = true;
            isTowerTwo = false;
            isTowerThree = false;
        }

        void LightningOnClick()
        {
            isTowerOne = false;
            isTowerTwo = true;
            isTowerThree = false;

        }

        void FreezeOnClick()
        {
            isTowerOne = false;
            isTowerTwo = false;
            isTowerThree = true;
        }
    }
}