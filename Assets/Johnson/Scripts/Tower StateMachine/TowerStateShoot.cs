using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class TowerStateShoot : TowerState
    {

        public override TowerState Update(TowerStateMachine tower)
        {

            if (tower.attackTarget != null)
            {
                tower.timeUntilNextShot -= Time.deltaTime;

                if (tower.timeUntilNextShot <= 0)
                {
                    tower.ShootProjectile();
                    tower.timeUntilNextShot = tower.timeBetweenShots;
                }
            }
            return null;
        }
    }
}