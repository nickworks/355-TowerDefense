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
    public class TowerStateIdle : TowerState
    {
        /// <summary>
        /// overrides the tower update to use in this current state
        /// </summary>
        /// <param name="tower">stores a reference of the TowerStateMachine in tower</param>
        /// <returns>It returns back into itself until something happens</returns>
        public override TowerState Update(TowerStateMachine tower)
        {
            tower.GetClosestEnemy(); // locates the closest enemy to the tower
            return null; // return back into itself
        } // end update
    } // end class
} // end namespace