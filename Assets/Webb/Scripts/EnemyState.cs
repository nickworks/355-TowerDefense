using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{
    public abstract class EnemyState
    {
        public abstract EnemyState Update(EnemyController1 enemy);
        public virtual void OnStart(EnemyController1 enemy) { }
        public virtual void OnEnd(EnemyController1 enemy) { }

    }
}
