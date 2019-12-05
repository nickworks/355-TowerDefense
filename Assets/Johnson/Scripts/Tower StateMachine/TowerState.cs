using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public abstract class TowerState
    {
        public abstract TowerState Update(Tower tower);

        public virtual void OnStart(Tower tower) { }
        public virtual void OnEnd(Tower tower) { }
    }
}