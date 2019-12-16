using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{/// <summary>
/// makes enemy attack object and sets back to chase after destrouying goal
/// </summary>
    public class EnemyStateAttack : EnemyState
    {
        // Start is called before the first frame update
        /// <summary>
        /// overides the current state
        /// </summary>
        /// <param name="enemy"></param> allows to call function from orginal scripts
        /// <returns></returns>
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