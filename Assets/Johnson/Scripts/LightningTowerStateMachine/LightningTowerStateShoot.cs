using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// This class/State handles the attack of the tower
    /// </summary>
    public class LightningTowerStateShoot : LightningTowerState
    {

        /// <summary>
        /// Overrides the lightning towers's update function to use to attack the targeted base
        /// </summary>
        /// <param name="lightningTower">stores a reference of the TowerStateMachine in tower</param>
        /// <returns>>It returns back into itself until the current enemy is destroyed</returns>
        public override LightningTowerState Update(LightningTowerStateMachine lightningTower)
        {
            if(lightningTower.attackTarget != null) // if the attackTarget isn't null
            {
                lightningTower.timeUntilNextShot -= Time.deltaTime;  // then start attack timer

                if (lightningTower.timeUntilNextShot <= 0)// if atack timer reaches zero
                {
                    lightningTower.enemy.TakeDamage(lightningTower.attackDamage); // attack
                    lightningTower.timeUntilNextShot = lightningTower.timeBetweenShots; // reset timer
                }
            }
            return null; // return back into itself
        }
    }
}
