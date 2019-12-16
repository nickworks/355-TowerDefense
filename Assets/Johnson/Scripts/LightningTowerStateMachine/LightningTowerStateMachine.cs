using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    public class LightningTowerStateMachine : MonoBehaviour
    {
        LightningTowerState currentState; // the currrent state of the enemy

        public float attackCooldown = 0.5f; // cooldown time for attacks
        public float attackDamage = 25; // attack damage of the tower

        [HideInInspector]
        public float timeBetweenShots = .5f; // this holds the time that the boss has to wait before firing again
        [HideInInspector]
        public float timeUntilNextShot = 2; // this hold the time until the next shot
        [HideInInspector]
        public EnemyStateMachine enemy; // hold a copy of the enemy

        List<EnemyStateMachine> enemies = new List<EnemyStateMachine>(); // this is a special array that holds all the enemies on scene

        [HideInInspector]
        public float timerAttackCooldown = 0; // current time for the attacks cooldown

        [HideInInspector]
        public Transform attackTarget { get; private set; }  // gets the position of the current attack target

        Vector3 vectorToTarget; // line to target

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;

            if (currentState == null) SwitchToState(new LightningTowerStateIdle());

            if (currentState != null) SwitchToState(currentState.Update(this));
        }

        /// <summary>
        /// this is the start function for the selector mechanic in the game... i broke it and haven't fixed it
        /// </summary>
        public void StartSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        /// <summary>
        /// end function for the selector
        /// </summary>
        public void EndSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        /// <summary>
        /// This function controls the switching of states
        /// </summary>
        /// <param name="newState">Passes in a copy of the state class to be filled with the info from the newState</param>
        private void SwitchToState(LightningTowerState newState)
        {
            if (newState != null)
            {
                if (currentState != null) currentState.OnEnd(this);
                currentState = newState;
                currentState.OnStart(this);
            }
        }

        /// <summary>
        /// This function activates when something enters the trigger collider
        /// </summary>
        /// <param name="collider">the collider of the object that entered the trigger is passed into the param</param>
        void OnTriggerEnter(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                attackTarget = e.transform;
                enemies.Add(e);

                if (attackTarget != null) SwitchToState(new LightningTowerStateShoot());
            }
            if (collider.GetComponent<EnemyStateMachine>() != null)
            {
                enemy = collider.GetComponent<EnemyStateMachine>();                               
            }

        }

        /// <summary>
        /// This function activates when something stays in the trigger collider
        /// </summary>
        /// <param name="collider">the collider of the object that entered the trigger is passed into the param</param>
        private void OnTriggerStay(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                attackTarget = e.transform;

                if (attackTarget != null) SwitchToState(new LightningTowerStateShoot());
            }
        }

        /// <summary>
        /// This function activates when something exits in the trigger collider
        /// </summary>
        /// <param name="collider">the collider of the object that entered the trigger is passed into the param</param>
        void OnTriggerExit(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                enemies.Remove(e);
                if (attackTarget == null) SwitchToState(new LightningTowerStateIdle());
            }

        }

        
    }
}