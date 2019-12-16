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
    public class TowerStateShoot : TowerState
    {
        /// <summary>
        /// Overrides the towers's update function to use to attack the targeted base
        /// </summary>
        /// <param name="tower">stores a reference of the TowerStateMachine in tower</param>
        /// <returns>>It returns back into itself until the current enemy is destroyed</returns>
        public override TowerState Update(TowerStateMachine tower)
        {

            if (tower.attackTarget != null) // if the attackTarget isn't null
            {
                tower.timeUntilNextShot -= Time.deltaTime; // then start attack timer

                if (tower.timeUntilNextShot <= 0) // if atack timer reaches zero
                {
                    tower.ShootProjectile(); // shoot
                    tower.timeUntilNextShot = tower.timeBetweenShots; // reset timer 
                }
            }
            return null; // return back into itself
        }
    }
}