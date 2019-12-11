using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


namespace Webb

{
    /// <summary>
    /// this enemy does
    /// </summary>
    public class EnemyController1 : MonoBehaviour
    {
        public float attackCooldown = 0.5f;
        public float attackDamge = 1;
        public ParticleSystem coins;
        NavMeshAgent agent;
        LineRenderer line;
        EnemyGoal goal;
        public Image healthBar;
        int health = 100;
        bool isAttackState;

        float timerAttackCooldown = 0;

        EnemyState currentState;



        // Start is called before the first frame update
        void Start()
        {


        }

        // Update is called once per frame

        void Update()
        {
            agent = GetComponent<NavMeshAgent>();
            line = GetComponent<LineRenderer>();
            coins.Stop();
            if (currentState == null) SwitchToState(new EnemyStateChase());
            if (currentState != null) SwitchToState(currentState.Update(this));


        }
        private void SwitchToState(EnemyState newState)
        {
            if (newState != null)
            {
                if (currentState != null) currentState.OnEnd(this);

                currentState = newState;
                currentState.OnStart(this);
            }
        }



        // Update is called once per frame



        /*   healthBar.fillAmount = health / 100;
                timerAttackCooldown -= Time.deltaTime;
                if (health <= 0) Destroy(gameObject);
                if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;
                if (isAttackState)
                {
                    if (goal)
                    {
                        if (timerAttackCooldown <= 0)
                            goal.TakeDamge(attackDamge);
                        //timerAttackCooldown = attackCooldown;
                    }
                    else
                    {
                        isAttackState = false;
                    }

                }*/
        public void Chase()
        {
            if (goal)
            {
                if (agent.isOnNavMesh) agent.destination = goal.transform.position;
                Vector3[] points = agent.path.corners;
                // line.postuionCount = points.l
                // line.SetPositions();


            }
            else
            {
                FindClosestGoal();
            }
        }

        private void FindClosestGoal()
        {
            float minDis = 0;
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

            void OnTriggerEnter(Collider trigger)
            {
                if (trigger.transform.tag == "Bullet")
                {
                    print("Oh shit ive been hit");
                    health -= 5;
                    GetComponent<NavMeshAgent>().speed = 1;
                }
                if (trigger.transform.tag == "AreaAffectBullet")
                {
                    print("Oh shit ive been hit");
                    health -= 10;
                }
                if (trigger.transform.tag == "Freeze")
                {
                    print("Oh shit ive been hit");
                    health -= 20;
                }
                if (trigger.GetComponent<EnemyGoal>() != null)
                {

                    isAttackState = true;
                    Tower tower = trigger.GetComponent<Tower>();
                    if (tower != null)
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
}
    
        
    



