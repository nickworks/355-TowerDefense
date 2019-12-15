using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class FreezeTowerStateShoot : FreezeTowerState
    {
        public override FreezeTowerState Update(FreezeTowerStateMachine freezeTower)
        {
            if (freezeTower.attackTarget != null)
            {
                freezeTower.timeUntilNextShot -= Time.deltaTime;

                if (freezeTower.timeUntilNextShot <= 0)
                {
                    freezeTower.enemy.agent.speed = .8f;
                    freezeTower.enemy.TakeDamage(freezeTower.attackDamage);
                    freezeTower.timeUntilNextShot = freezeTower.timeBetweenShots;
                    freezeTower.enemy.isUnfrozen = false;
                }
                
            }
            return null;
        }
    }
}