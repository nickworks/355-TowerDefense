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
        void Update()
        {
            mesh.materials[0] = frozenMat;
            frozenTimer += Time.deltaTime;
            if (frozenTimer >= freezeDuration)
            {
                mesh.materials[0] = defMat;
                isFrozen = false;
                frozenTimer = 0;
            }
            return;
        }
    }
}