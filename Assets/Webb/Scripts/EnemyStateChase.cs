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


            return null;
        }
    }
}
        // Start is called before the first frame update
