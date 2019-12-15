using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public abstract class LightningTowerState
    {
        public abstract LightningTowerState Update(LightningTowerStateMachine lightningTower);

        public virtual void OnStart(LightningTowerStateMachine lightningTower) { }
        public virtual void OnEnd(LightningTowerStateMachine lightningTower) { }
    }
}