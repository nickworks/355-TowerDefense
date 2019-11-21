using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Wiles
{
    public class EnemyController : MonoBehaviour
    {

        public float attackCooldown = 0.5f;
        public float attackDamage = 25;

        NavMeshAgent agent;
        EnemyGoal goal;
        bool isAttackState = false;
        private float timerAttackCooldown = 0;


        // Start is called before the first frame update
        void Start()
        {

            agent = GetComponent<NavMeshAgent>();


        }

        // Update is called once per frame
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

                } else
                {
                    isAttackState = false;
                }
            }

            if(goal)
            {
                agent.destination = goal.transform.position;
            } else
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
                float dis = (g.transform.position - transform.position).magnitude; // distance to EnemyGoal g

                if (dis < minDis || goal == null)
                {
                    goal = g;
                    minDis = dis;
                }

            }
        }


        void OnTriggerEnter(Collider trigger)
        {
            //print("enemy entered a trigger volume");

            if(trigger.GetComponent<EnemyGoal>() != null)
            {
                isAttackState = true;
            }
            /*
            Tower tower = trigger.GetComponent<Tower>();
            if(tower != null)
            {
                print("enemy close to a tower");
            }*/
        }
        void OnTriggerExit(Collider trigger)
        {
            //print("enemy exited a trigger volume");

            if (trigger.GetComponent<EnemyGoal>() != null)
            {
                isAttackState = false;
            }

        }
    }
}