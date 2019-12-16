using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson {

    /// <summary>
    /// this class holds all the code for controlling the HUD
    /// </summary>
    public class HUDController : MonoBehaviour
    {

        public Transform panelMain; // holds copy of main UI panel in scene... left one
        public Transform panelUpgrade; // when a tower is selected this panel comes up on right side of screen
        public Text panelUpgradeText; // holds text info in this

        /// <summary>
        /// the constructor function
        /// </summary>
        void Start()
        {

        }

        /// <summary>
        /// the game loop, or update function
        /// </summary>
        void Update()
        {

            if (ClickToSpawnTower.currentlySelectedTower != null) // a tower is selected
            {
                panelUpgrade.gameObject.SetActive(true); // bring up the upgrade panel
                panelUpgradeText.text = ClickToSpawnTower.currentlySelectedTower.gameObject.name; // set panel text to the selected towers name
            }
            else
            {
                panelUpgrade.gameObject.SetActive(false); // keep upgrade panel hidden
            }
        }

        /// <summary>
        /// if the first upgrade button is pressed, upgrade tower
        /// </summary>
        public void TriggerUpgrade1()
        {
            if (ClickToSpawnTower.currentlySelectedTower != null)
            {
                //ClickToSpawnTower.currentlySelectedTower.UpgradeTower();
            }
        }
    }
}