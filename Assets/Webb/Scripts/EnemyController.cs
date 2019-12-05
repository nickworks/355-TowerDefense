using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Webb
{
    public class EnemyController : MonoBehaviour
    {
public float attackCooldown = 0.5f;
        public float attackDamge = 1;
        public ParticleSystem coins;
        NavMeshAgent agent;
        LineRenderer line;
        EnemyGoal goal;
        int health = 10;
        bool isAttackState;
        
        float timerAttackCooldown = 0;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            line = GetComponent<LineRenderer>();
            coins.Stop();
           
        }

        // Update is called once per frame
        void Update()
        {
            if (health <= 0) Destroy(gameObject);
            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;
            if (isAttackState)
            {
                if (goal)
                {
                    if(timerAttackCooldown <= 0)
                    goal.TakeDamge(attackDamge);
                   // timerAttackCooldown = attackCooldown;
                }
                else
                {
                    isAttackState = false;
                }

            }
            if (goal) {
              if(agent.isOnNavMesh)  agent.destination = goal.transform.position;
                Vector3[] points = agent.path.corners;
                line.postuionCount = points.l
                line.SetPositions();
                
             
            }
            else
            {
 FindClosestGoal();
            }
        }

        private void FindClosestGoal()
        {    float minDis = 0;
            if (goal)
            {
                agent.destination = goal.transform.position;
            }
            else
            {
                EnemyGoal[] goals = GameObject.FindObjectsOfType<EnemyGoal>();
                foreach (EnemyGoal g in goals)
                {
                    
                    float dis = (g.transform.position - transform.position).magnitude;// distance to EnemyGoal g
                    if (dis < minDis || goal == null)
                    {
                        goal = g;
                        minDis = dis;

                    }
                }
            }
        }
        void OnTriggerEnter(Collider trigger)
        { 
            if (trigger.transform.tag == "Bullet")
                {
                    print("Oh shit ive been hit");
                health -= 2;
                }
            if(trigger.GetComponent<EnemyGoal>() != null)
            {
              
                isAttackState = true;
                Tower tower = trigger.GetComponent<Tower>();
                if(tower != null)
                {
                    print("enemy close to tower");
                }
               
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
