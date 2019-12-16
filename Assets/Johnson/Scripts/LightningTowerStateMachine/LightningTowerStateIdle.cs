using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// This class is responsible for the idle state controls for this tower
    /// </summary>
    public class LightningTowerStateIdle : LightningTowerState
    {
        /// <summary>
        /// Overrides the lightning tower update to use in this current state
        /// </summary>
        /// <param name="lightningTower">stores a reference of the LightningTowerStateMachine in lightningTower</param>
        /// <returns>It returns back into itself until the current enemy is destroyed or out of range</returns>
        public override LightningTowerState Update(LightningTowerStateMachine lightningTower)
        {
            return null;// stay in current state
        } // end update
    } // end class
} // end namespace