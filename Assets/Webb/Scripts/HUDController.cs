using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Webb
{
    public class HUDController : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform panelMain;
        public Transform panelUpgrade;
        public Text panelUpgradeText;
        void Start()
        {

        }
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
  
