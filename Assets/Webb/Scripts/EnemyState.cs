using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{/// <summary>
/// stets up the stated deign pattern
/// </summary>
    public abstract class EnemyState
    {
        public abstract EnemyState Update(EnemyController1 enemy);
        public virtual void OnStart(EnemyController1 enemy) { }
        public virtual void OnEnd(EnemyController1 enemy) { }

    }
}
