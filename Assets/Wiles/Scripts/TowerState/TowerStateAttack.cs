using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class TowerStateAttack : TowerState
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public override TowerState Update(TowerController tower)
        {
            if (tower.Zap(tower.GetClosestEnemy()) == true)
            {

                switch (tower.currentAttack)
                {
                    case TowerController.AttackType.Zap:
                        tower.Zap(tower.GetClosestEnemy());
                        break;
                    case TowerController.AttackType.Projectile:
                        tower.Shoot(tower.GetClosestEnemy());
                        break;
                    case TowerController.AttackType.Ice:
                        tower.Freeze(tower.GetClosestEnemy());
                        break;
                    default:
                        break;
                }

                return new TowerStateIdle();
            }
            else return new TowerStateSearch();
        }
    }
}