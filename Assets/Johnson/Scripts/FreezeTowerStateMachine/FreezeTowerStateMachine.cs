using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class FreezeTowerStateMachine : MonoBehaviour
    {
        FreezeTowerState currentState;

        public float attackCooldown = 0.7f;
        public float attackDamage = 15;

        [HideInInspector]
        public float timeBetweenShots = .5f; // this holds the time that the boss has to wait before firing again
        [HideInInspector]
        public float timeUntilNextShot = 2; // this hold the time until the next shot
        [HideInInspector]
        public EnemyStateMachine enemy;

        List<EnemyStateMachine> enemies = new List<EnemyStateMachine>();

        [HideInInspector]
        public float timerAttackCooldown = 0;

        [HideInInspector]
        public Transform attackTarget { get; private set; }

        Vector3 vectorToTarget;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;

            if (currentState == null) SwitchToState(new FreezeTowerStateIdle());

            if (currentState != null) SwitchToState(currentState.Update(this));
        }

        public void StartSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        public void EndSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        private void SwitchToState(FreezeTowerState newState)
        {
            if (newState != null)
            {
                if (currentState != null) currentState.OnEnd(this);
                currentState = newState;
                currentState.OnStart(this);
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                attackTarget = e.transform;
                enemies.Add(e);

                if (attackTarget != null) SwitchToState(new FreezeTowerStateShoot());
            }
            if (collider.GetComponent<EnemyStateMachine>() != null)
            {
                enemy = collider.GetComponent<EnemyStateMachine>();
            }

        }
        private void OnTriggerStay(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                attackTarget = e.transform;

                if (attackTarget != null) SwitchToState(new FreezeTowerStateShoot());
            }
        }
        void OnTriggerExit(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                enemies.Remove(e);
                if (attackTarget == null) SwitchToState(new FreezeTowerStateIdle());
            }

        }
    }
}