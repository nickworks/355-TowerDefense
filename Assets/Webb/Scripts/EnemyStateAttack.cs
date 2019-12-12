using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{
    public class EnemyStateAttack : EnemyState
    {
        // Start is called before the first frame update
        public override EnemyState Update(EnemyController1 enemy)
        {

            enemy.Attack();
            enemy.EnemyHealth();
            if (enemy.isAttackState == false)
            {
                return new EnemyStateChase();
            }

            return null;
        }
    }
}