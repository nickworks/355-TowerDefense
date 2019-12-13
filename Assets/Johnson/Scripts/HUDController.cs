using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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