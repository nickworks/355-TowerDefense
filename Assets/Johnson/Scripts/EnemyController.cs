using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Johnson
{
    public class EnemyController : MonoBehaviour
    {

        public float attackCooldown = 0.5f;
        public float attackDamage = 25;


        NavMeshAgent agent;
        EnemyGoal goal;
        bool isAttackState = false;
        float timerAttackCooldown = 0;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }
                
        void Update()
        {

            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;

            if (isAttackState)
            {

                if (goal)
                {
                    if(timerAttackCooldown <= 0)
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
            
            if (goal)
            {
                agent.destination = goal.transform.position;
            }
            else
            {
                FindClosestGoal();
            }

            

        }

        void FindClosestGoal()
        {
            EnemyGoal[] goals = GameObject.FindObjectsOfType<EnemyGoal>();

            float minDis = 0;
            foreach (EnemyGoal g in goals)
            {
                float dis = (g.transform.position - transform.position).magnitude; // distance to enemy goal g

                if (dis < minDis || goal == null)
                {
                    goal = g;
                    minDis = dis;
                }
            }
        }

        void OnTriggerEnter(Collider trigger)
        {
           
            if(trigger.GetComponent<EnemyGoal>() != null)
            {
                isAttackState = true;
            }
            
        }

        void OnTriggerExit(Collider trigger)
        {
            
            if (trigger.GetComponent<EnemyGoal>() != null)
            {
                isAttackState = false;
            }
        }
    }
}