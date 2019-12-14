using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public abstract class TowerState
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public abstract TowerState Update(TowerController tower);

        public virtual void OnStart(TowerController tower) { }
        public virtual void OnEnd(TowerController tower) { }
    }
}