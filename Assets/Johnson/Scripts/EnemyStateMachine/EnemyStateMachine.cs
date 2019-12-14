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

        EnemyState currentState;
        [HideInInspector]
        public LineRenderer line;
        [HideInInspector]
        public NavMeshAgent agent;
        [HideInInspector]
        public EnemyGoal goal;

        [HideInInspector]
        public float health = 100;
        public Image healthBar;

        public float attackCooldown = 0.5f;
        public float attackDamage = 25;
        [HideInInspector]
        public float timerAttackCooldown = 0;

        bool isAttackState = false;
        public bool isDead // is a property not a field, so it wont show up in editor
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
            line = GetComponent<LineRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;

            if (currentState == null) SwitchToState(new EnemyStateIdle());

            if (currentState != null) SwitchToState(currentState.Update(this));

            if (isDead)
            {
                SwitchToState(new EnemyStateDead());
            }
        }

        private void SwitchToState(EnemyState newState)
        {
            if(newState != null)
            {
                if (currentState != null) currentState.OnEnd(this);
                currentState = newState;
                currentState.OnStart(this);
            }
        }

        void OnTriggerEnter(Collider trigger)
        {

            if (trigger.GetComponent<EnemyGoal>() != null)
            {
                SwitchToState(new EnemyStateAttack());
            }

        }

        private void OnTriggerStay(Collider trigger)
        {
            if (trigger.GetComponent<EnemyGoal>() != null)
            {
                SwitchToState(new EnemyStateAttack());
            }

        }

        void OnTriggerExit(Collider trigger)
        {

            if (trigger.GetComponent<EnemyGoal>() != null)
            {
                SwitchToState(new EnemyStateIdle());
            }
        }

        public void FindClosestGoal()
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

        public void TakeDamage(float amount)
        {

            health -= amount;
            healthBar.fillAmount = (health / 100);
        }
        public void Explode()
        {
            print("BOOM!");
            // TODO: spawn particles
            // TODO: Play audio
            Destroy(gameObject);
        }

    }
}