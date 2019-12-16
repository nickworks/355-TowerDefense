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
    public class FreezeTowerStateIdle : FreezeTowerState
    {
        /// <summary>
        /// Overrides the freeze tower update to use in this current state
        /// </summary>
        /// <param name="freezeTower">stores a reference of the FreezeTowerStateMachine in freezeTower</param>
        /// <returns>It returns back into itself until something happens</returns>
        public override FreezeTowerState Update(FreezeTowerStateMachine freezeTower)
        {
            return null;// stay in current state
        } // end update
    } // end class
} // end namespace