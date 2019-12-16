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
    public class FreezeTowerStateShoot : FreezeTowerState
    {
        public override FreezeTowerState Update(FreezeTowerStateMachine freezeTower)
        {
            if (freezeTower.attackTarget != null) // if attack target isnt null
            {
                freezeTower.timeUntilNextShot -= Time.deltaTime; // start timer

                if (freezeTower.timeUntilNextShot <= 0)
                {
                    freezeTower.enemy.agent.speed = 0f; // stop enemy movement
                    if (freezeTower.enemy.isUnfrozen)
                    {
                        freezeTower.enemy.isUnfrozen = false; // freeze enemy
                    }
                    freezeTower.enemy.TakeDamage(freezeTower.attackDamage); // attack
                    freezeTower.timeUntilNextShot = freezeTower.timeBetweenShots; // reset Timer
                    
                }
                
            }
            return null; // return back into itself
        }
    }
}