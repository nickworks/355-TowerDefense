using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class TowerStateShoot : TowerState
    {
        float timeBetweenShots = .5f;
        float timeUntilNextShot = 2;

        public override TowerState Update(Tower tower)
        {
            timeUntilNextShot -= Time.deltaTime;

            if(timeUntilNextShot <= 0)
            {
                tower.ShootProjectile();
                timeUntilNextShot = timeBetweenShots;
            }

            return null;
        }
    }
}