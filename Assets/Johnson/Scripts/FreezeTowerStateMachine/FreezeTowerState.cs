﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// This class is an empty class for it to be taken over by other classes, for the state machine
    /// </summary>
    public abstract class FreezeTowerState
    {
        public abstract FreezeTowerState Update(FreezeTowerStateMachine freezeTower);// creates a special update that can be taken over by other classes

        public virtual void OnStart(FreezeTowerStateMachine freezeTower) { } // a start function that can be taken over by other classes
        public virtual void OnEnd(FreezeTowerStateMachine freezeTower) { } // a end function that can be taken over by other classes
    } // end class
} // end namespace