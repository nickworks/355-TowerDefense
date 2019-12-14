using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class TowerStateAttack : TowerState
    {

        float timer = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public override TowerState Update(TowerController tower)
        {
            if (tower.Attack(tower.GetClosestEnemy()) == true) return new TowerStateIdle();
            else return new TowerStateSearch();
        }
    }
}