using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// This class/State handles when the enemies die
    /// </summary>
    public class EnemyStateDead : EnemyState
    { 
        /// <summary>
        /// Overrides the enemy's update function to use
        /// </summary>
        /// <param name="enemy"> stores a reference of the EnemyStateMachine in enemy</param>
        /// <returns>nothing because the enemy is dead</returns>
        public override EnemyState Update(EnemyStateMachine enemy)
        {
            //Debug.Log("boom"); 
            enemy.Explode(); // tells the enemy to explode(destroy itself)
            return null; // stay in this state
        } // end update
    } // end class
} // end namespace