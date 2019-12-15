using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace White
{
    /// <summary>
    /// This class controls the enemys' behavior.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        /// <summary>
        /// The time it takes before the next attack stops.
        /// </summary>
        public float attackCooldown = 0.5f;

        /// <summary>
        /// The amount of damage dealt with each attack.
        /// </summary>
        public float attackDamage = 25;

        /// <summary>
        /// Where the enemy is allowed to move.
        /// </summary>
        NavMeshAgent agent;

        /// <summary>
        /// Where the enemy is moving to.
        /// </summary>
        EnemyGoal goal;

        /// <summary>
        /// Whether or not the enemy is currently attacking.
        /// </summary>
        bool isAttackState = false;

        /// <summary>
        /// The time it takes before the next attack is initiated.
        /// </summary>
        float timerAttackCooldown = 0;
        
        /// <summary>
        /// This function gets where the enemy is allowed to move.
        /// </summary>
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        } // ends the Start() function
        
        /// <summary>
        /// This function updates each enemy.
        /// </summary>
        void Update()
        {
            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;

            if(isAttackState)
            {
                if (isAttackState)
                {
                    if (goal)
                    {
                        if (timerAttackCooldown <= 0)
                        {
                            goal.TakeDamage(attackDamage);
                            timerAttackCooldown = attackCooldown;
                        }
                    }
                    else
                    {
                        isAttackState = false;
                    }
                }
            }

            if(goal)
            {
                agent.destination = goal.transform.position;
            }
            else
            {
                FindClosestGoal();
            }
        } // ends the Update() function

        /// <summary>
        /// This function finds the closest base to attack.
        /// </summary>
        private void FindClosestGoal()
        {
            EnemyGoal[] goals = GameObject.FindObjectsOfType<EnemyGoal>();

            float minDis = 0;
            foreach (EnemyGoal g in goals)
            {
                float dis = (g.transform.position - transform.position).magnitude; // distance to EnemyGoal g

                if (dis < minDis || goal == null)
                {
                    goal = g;
                    minDis = dis;
                }
            }
        } // ends the FindClosestGoal() function

        /// <summary>
        /// This function sets the enemy into it's attack state.
        /// </summary>
        /// <param name="trigger">The trigger that sets isAttackState to true.</param>
        void OnTriggerEnter(Collider trigger)
        {
            if(trigger.GetComponent<EnemyGoal>() != null)
            {
                isAttackState = true;
            }
        } // ends the OnTriggerEnter() function

        /// <summary>
        /// This function sets the enemy's attack state equal to false.
        /// </summary>
        /// <param name="trigger">The trigger that sets isAttackState to false.</param>
        void OnTriggerExit(Collider trigger)
        {
            if (trigger.GetComponent<EnemyGoal>() != null)
            {
                isAttackState = false;
            }
        } // ends the OnTriggerExit() function
    } // ends the EnemyController class
} // ends the White namespace