using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Wiles
{
    public class EnemyController : MonoBehaviour
    {
        public float health = 250;
        public float attackDamage = 25;
        public float attackRange = 0;
        public float attackCooldown = 0.5f;
        public float speed = 2.5f;


        NavMeshAgent agent;
        EnemyGoal goal;
        bool isAttackState = false;
        private float timerAttackCooldown = 0;

        public bool isDead
        {
            get
            {
                return (health <= 0);
            }
        }
        // Start is called before the first frame update
        void Start()
        {

            agent = GetComponent<NavMeshAgent>();


        }

        // Update is called once per frame
        void Update()
        {

            if (isDead) Explode();

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

        private void Explode()
        {
            print("ENEMY BOOM");
            // TODO: spawn particles
            // TODO: play audio
            Destroy(gameObject);
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
        public void TakeDamage(float amount)
        {
            health -= amount;
        }
    }
}