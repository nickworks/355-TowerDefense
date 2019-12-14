using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson {
    public class HUDController : MonoBehaviour
    {

        public Transform panelMain;
        public Transform panelUpgrade;
        public Text panelUpgradeText;


        void Start()
        {

        }

        void Update()
        {

            if (ClickToSpawnTower.currentlySelectedTower != null)
            {
                panelUpgrade.gameObject.SetActive(true);
                panelUpgradeText.text = ClickToSpawnTower.currentlySelectedTower.gameObject.name;
            }
            else
            {
                panelUpgrade.gameObject.SetActive(false);
            }
        }

        public void TriggerUpgrade1()
        {
            if (ClickToSpawnTower.currentlySelectedTower != null)
            {
                //ClickToSpawnTower.currentlySelectedTower.UpgradeTower();
            }
        }
    }
}