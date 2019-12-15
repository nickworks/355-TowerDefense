using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


namespace Webb

{
    /// <summary>
    /// this enemy does
    /// it stes up functions to chase enemy goals and attack enemy base
    /// sets up swithcing up differt states
    /// </summary>
    public class EnemyController1 : MonoBehaviour
    {
        public float attackCooldown = 0.5f;// how long before next attack
        public float attackDamge = 1; // how much damge each attack does
        bool slowed;// checks if enemy is slowed
        public ParticleSystem coins;// particles of coin
        NavMeshAgent agent; // how enemy moves
        LineRenderer line;// shows the path
       public EnemyGoal goal;  // passes in whaht enemy to attak
        public Image healthBar;// gets refrence to helath bar
        int health = 100; //keeps track of current health
      public  bool isAttackState;// checks if its attacking

        float timerAttackCooldown = 0; // keeps track of how long before next attack

        EnemyState currentState;// keeps track of what state 



        // Start is called before the first frame update
        void Start()
        {
coins.Stop();

        }

        // Update is called once per frame

        void Update()
        {
            agent = GetComponent<NavMeshAgent>();
            line = GetComponent<LineRenderer>();
            
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


        public void EnemyHealth() {
            healthBar.fillAmount = health / 100;
            if (health <= 0) Destroy(gameObject); 
                }
            public void Attack () {
           // timerAttackCooldown -= Time.deltaTime;  
               // if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;
                     //   if (timerAttackCooldown <= 0)
                            goal.TakeDamge(attackDamge);
                        //timerAttackCooldown = attackCooldown;
                        }
              
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

            
    }
        void OnTriggerEnter(Collider trigger)
            {
                if (trigger.transform.tag == "Dark")
                {
                    print("Oh shit ive been hit");
                    health -= 50;
                   
                }
                if (trigger.transform.tag == "Nature")
                {
                    print("Oh shit ive been hit");
                    health -= 30;
                }
                if (trigger.transform.tag == "Light")
                {
                    print("Oh shit ive been hit");
                    health -= 1; 
                if (slowed == false) GetComponent<NavMeshAgent>().speed *= .8f;
                slowed =  true;

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
    
        
    



