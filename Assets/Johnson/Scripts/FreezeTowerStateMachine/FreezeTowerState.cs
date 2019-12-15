using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public abstract class FreezeTowerState
    {
        public abstract FreezeTowerState Update(FreezeTowerStateMachine freezeTower);

        public virtual void OnStart(FreezeTowerStateMachine freezeTower) { }
        public virtual void OnEnd(FreezeTowerStateMachine freezeTower) { }
    }
}