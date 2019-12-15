using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class TowerStateIdle : TowerState
    {


        public override TowerState Update(TowerStateMachine tower)
        {
            tower.GetClosestEnemy();
            return null;
        }

    }
}