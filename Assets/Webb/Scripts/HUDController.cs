using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// changes the hud based on whats clicked
namespace Webb
{
    public class HUDController : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform panelMain;// refrence to panal main
        public Transform panelUpgrade;// refrence to panal upgrad
        public Text panelUpgradeText; // refrence to panalupgradetext
        void Start()
        {

        }
        /// <summary>
        /// looks for what tower is selceted
        /// </summary>
        public void TriggerUpgrade1()
        {
            if (ClickToSpawnTower.currentlySelectedTower != null)
            {
                //ClickToSpawnTower.currentlySelectedTower.UpgradeTower();
               
            }
        }
        // Update is called once per frame
        void Update()
        {
           /** if(ClickToSpawnTower.currentlySelectedTower != null)
            {
                panelUpgrade.gameObject.SetActive(true); 
                panelUpgradeText.text = ClickToSpawnTower.currentlySelectedTower.gameObject.name;
            }
            else
            {
                panelUpgrade.gameObject.SetActive(false);
            }
    */
        }

    }

    }
  
