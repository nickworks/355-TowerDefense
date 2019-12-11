using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wiles
{
    public class GUI : MonoBehaviour
    {

        //string totalHealth = "Total Health: ";
        //string money = "$";
        //string currentRound = "Round: #/40";
        //string currentTowerToBuy = "Tower Name";
        //string playSpeed = "||>  |>  |>|>";

        public Tower basicZapTower;
        public Tower basicProjectileTower;
        public Tower iceTower;

        public Text totalHealth;
        public Text money;
        public Text currentRound;
        public Text currentTowerToBuy;
        public Text playSpeed;

        public Camera cam;
        ClickToSpawnTower ctpt;

        // Start is called before the first frame update
        void Start()
        {
            ctpt = cam.GetComponent<ClickToSpawnTower>();
        }

        // Update is called once per frame
        void Update()
        {
            if(ctpt.currentTowerToBuy != null) currentTowerToBuy.text = ctpt.currentTowerToBuy.ToString();
        }
    }
}