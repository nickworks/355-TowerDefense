using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// The Enemies State Machine
    /// </summary>
    public class EnemyStateMachine : MonoBehaviour
    {

        EnemyState currentState; // the currrent state of the enemy

        [HideInInspector] // hides public properties from the inspector
        public LineRenderer line; // The renderer for the enemy path

        [HideInInspector] // hides public properties from the inspector
        public NavMeshAgent agent; // The agent that will traverse the NavMesh

        [HideInInspector] // hides public properties from the inspector
        public EnemyGoal goal; // is the goal that the enemies navigate to

        [HideInInspector]
        public float health = 100; // current health of the enemy

        public Image healthBar; // holds a copy of the healthbar GUI so that it can be updated to show users the enemies health

        public float attackCooldown = 0.5f; // cooldown time for attacks

        public float attackDamage = 25; // attack damage of the enemy

        public bool isUnfrozen = true;
        float frozenTimer = 1.0f;
        float frozenTimerMax = 1.0f;


        [HideInInspector]
        public float timerAttackCooldown = 0; // current time for the attacks cooldown
        
        public bool isDead // is a property not a field, so it wont show up in editor
        {
            get // gets the desired info
            {
                return (health <= 0); // return false if health is less than or equal to zero
            }
        }

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            line = GetComponent<LineRenderer>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>        
        void Update()
        {
            if (isUnfrozen == false) // if the enemy is frozen
            {
                frozenTimer -= Time.deltaTime; // count down defrost time

                if (frozenTimer <= 0) // if defrost time hits 0 or less
                {
                    agent.speed = 3.5f; // return normal speed
                    isUnfrozen = true; // is no longer frozen
                    frozenTimer = frozenTimerMax; // reset the timer
                } // end if

            } // end if

            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime; // countdown timer for enemy attack

            if (currentState == null) SwitchToState(new EnemyStateIdle()); // if currentState is empty, then switch to idle state

            if (currentState != null) SwitchToState(currentState.Update(this)); // if currentState is not null, then update the current state

            if (isDead) // if isDead is true
            {
                SwitchToState(new EnemyStateDead()); // then switch to death state
            }
        } // end update

        /// <summary>
        /// This function controls the switching of states
        /// </summary>
        /// <param name="newState">Passes in a copy of the state class to be filled with the info from the newState</param>
        private void SwitchToState(EnemyState newState)
        {
            if(newState != null) // if newState is not null
            {
                if (currentState != null) currentState.OnEnd(this); // if the currentState is not null, then activate the OnEnd Funtion
                currentState = newState; // currentState then passed the new state
                currentState.OnStart(this); // Activate the OnStart function of the new state
            }
        } // end SwitchToState

        /// <summary>
        ///  This function activates when something enters the trigger collider
        /// </summary>
        /// <param name="trigger">the collider of the object that entered the trigger is passed into the param</param>
        void OnTriggerEnter(Collider trigger)
        {

            if (trigger.GetComponent<EnemyGoal>() != null) // if the collider that enters the trigger is the enemy goal
            {
                SwitchToState(new EnemyStateAttack()); // switch to attack state
            }

        } // end OnTriggerEnter

        /// <summary>
        ///  This function activates when something stays the trigger collider
        /// </summary>
        /// <param name="trigger">the collider of the object that entered the trigger is passed into the param</param>
        private void OnTriggerStay(Collider trigger)
        {
            if (trigger.GetComponent<EnemyGoal>() != null) // if the collider that stays in the trigger is the enemy goal
            {
                SwitchToState(new EnemyStateAttack());// switch to attack state
            }

        }// end OnTriggerStay

        /// <summary>
        ///  This function activates when something exits the trigger collider
        /// </summary>
        /// <param name="trigger">the collider of the object that entered the trigger is passed into the param</param>
        void OnTriggerExit(Collider trigger)
        {
            if (trigger.GetComponent<EnemyGoal>() != null) // if the collider that exits the trigger is the enemy goal
            {
                SwitchToState(new EnemyStateIdle()); // switch to idle state
            }
        }// end OnTriggerExit

        /// <summary>
        /// This function gets the closest target for the enemy to attack
        /// </summary>
        public void FindClosestGoal()
        {
            EnemyGoal[] goals = GameObject.FindObjectsOfType<EnemyGoal>(); // array that holds all the goals that the enemy can choose from

            float minDis = 0; // holds the minnimum distance from target for the enemy to attack 
            foreach (EnemyGoal g in goals)
            {
                float dis = (g.transform.position - transform.position).magnitude; // distance to enemy goal g

                if (dis < minDis || goal == null) // if the distance is less than the min dist or goal equals null
                {
                    goal = g; // set goal to g
                    minDis = dis; // minDis equals current distance
                }
            }
        } // end FindClosestGoal

        /// <summary>
        /// This function controls the damage intake of the enemy
        /// </summary>
        /// <param name="amount">This is that incoming damage that the enemy is taking</param>
        public void TakeDamage(float amount)
        {
            health -= amount; // subtract health from attacked amount
            healthBar.fillAmount = (health / 100); // fill the health bar with the current health
        } // end TakeDamage

        /// <summary>
        /// This function is called when the enemy dies, this is the death animation
        /// </summary>
        public void Explode()
        {
            //print("BOOM!");
            // TODO: spawn particles
            // TODO: Play audio
            Destroy(gameObject); // eliminate enemy game object
        } // end Explode

    } // end class
} // end namespace