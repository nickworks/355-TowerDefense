using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{
    public class EnemyStateChase : EnemyState
    {
        public override EnemyState Update(EnemyController1 enemy)
        {


            enemy.Chase();
            enemy.EnemyHealth();
            if (enemy.isAttackState == true)
            {
                return new EnemyStateAttack();
            }

            return null;
        }
    }
}
        // Start is called before the first frame update
