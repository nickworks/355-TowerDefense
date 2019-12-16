﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class TowerStateIdle : TowerState
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public override TowerState Update(TowerController tower)
        {
            tower.atkTimer += Time.deltaTime;
            if (tower.atkTimer >= tower.attackSpeed) return new TowerStateSearch();
            return null;
        }
    }
}