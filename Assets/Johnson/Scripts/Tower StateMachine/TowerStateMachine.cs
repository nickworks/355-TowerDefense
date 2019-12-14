using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    public class TowerStateMachine : MonoBehaviour
    {

        TowerState currentState;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (currentState == null) SwitchToState(new TowerStateIdle());

            //if (currentState != null) SwitchToState(currentState.Update(this));
        }

        private void SwitchToState(TowerState newState)
        {
            if (newState != null)
            {
                //if (currentState != null) currentState.OnEnd(this);
                //currentState = newState;
                //currentState.OnStart(this);
            }
        }
    }
}