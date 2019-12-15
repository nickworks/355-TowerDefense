using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class LightningTowerStateShoot : LightningTowerState
    {
        public override LightningTowerState Update(LightningTowerStateMachine lightningTower)
        {
            if(lightningTower.attackTarget != null)
            {
                lightningTower.timeUntilNextShot -= Time.deltaTime;

                if (lightningTower.timeUntilNextShot <= 0)
                {
                    lightningTower.enemy.TakeDamage(lightningTower.attackDamage);
                    lightningTower.timeUntilNextShot = lightningTower.timeBetweenShots;
                }
            }
            return null;
        }
    }
}
