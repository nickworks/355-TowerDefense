using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// This class is an empty class for it to be taken over by other classes, for the state machine
    /// </summary>
    public abstract class EnemyState
    {


        public abstract EnemyState Update(EnemyStateMachine enemy);

        public virtual void OnStart(EnemyStateMachine enemy) { }
        public virtual void OnEnd(EnemyStateMachine enemy) { }
    }
}