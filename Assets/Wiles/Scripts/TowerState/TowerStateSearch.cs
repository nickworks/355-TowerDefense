using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class TowerStateSearch : TowerState
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public override TowerState Update(TowerController tower)
        {
            if (tower.GetClosestEnemy() != null)
            {
                Debug.Log("target locked");
                
                tower.atkTimer = 0;

                return new TowerStateAttack();
            }
            else Debug.Log("No enemy");
            return null;
        }
    }
}