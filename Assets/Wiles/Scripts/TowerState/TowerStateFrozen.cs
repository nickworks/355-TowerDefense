using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class TowerStateFrozen : TowerState
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public override TowerState Update(TowerController tower)
        {
            
            
            tower.frozenTimer += Time.deltaTime;
            if (tower.frozenTimer >= tower.freezeDuration)
            {
                tower.isFrozen = false;
                tower.frozenTimer = 0;
                return tower.previousState;
            }
            
            return null;
        }
        
        public override void OnStart(TowerController tower) {
            if(tower.frozenMat != null) tower.mesh.sharedMaterial = tower.frozenMat;
        }

        public override void OnEnd(TowerController tower)
        {
            if (tower.defMat != null) tower.mesh.sharedMaterial = tower.defMat;
        }

    }
}