using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    public class TowerStateMachine : MonoBehaviour
    {

        TowerState currentState;

        public float attackCooldown = 0.5f;
        public float attackDamage = 25;

        [HideInInspector]
        public float timeBetweenShots = .5f; // this holds the time that the boss has to wait before firing again
        [HideInInspector]
        public float timeUntilNextShot = 2; // this hold the time until the next shot

        List<EnemyStateMachine> enemies = new List<EnemyStateMachine>();

        [HideInInspector]
        public float timerAttackCooldown = 0;

        [HideInInspector]
        public Transform attackTarget { get; private set; }

        public Projectile prefabProjectile;

        Vector3 vectorToTarget;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;

            if (currentState == null) SwitchToState(new TowerStateIdle());

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

        private void SwitchToState(TowerState newState)
        {
            if (newState != null)
            {
                if (currentState != null) currentState.OnEnd(this);
                currentState = newState;
                currentState.OnStart(this);
            }
        }

        public EnemyStateMachine GetClosestEnemy()
        {
            EnemyStateMachine result = null;
            float minDis = 0;
            // find closest

            foreach (EnemyStateMachine e in enemies)
            {
                float dis = (e.transform.position - transform.position).magnitude; // distance from tower to enemy

                if (dis < minDis || result == null)
                {
                    result = e;
                    minDis = dis;


                }
            }
            
            return result;
        }

        EnemyStateMachine GetRandomEnemy()
        {

            if (enemies.Count <= 0) return null;
            int index = Random.Range(0, enemies.Count);
            return enemies[index];
        }

        void OnTriggerEnter(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                attackTarget = e.transform;
                enemies.Add(e);

                if(attackTarget != null) SwitchToState(new TowerStateShoot());
            }

        }
        private void OnTriggerStay(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                attackTarget = e.transform;

                if (attackTarget != null)  SwitchToState(new TowerStateShoot());
            }
        }
        void OnTriggerExit(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                enemies.Remove(e);
                if (attackTarget == null)  SwitchToState(new TowerStateIdle());
            }

        }

        public Vector3 VectorToAttackTarget()
        {
                return attackTarget.position - new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        }

        public float DistanceToAttackTarget()
        {
            return VectorToAttackTarget().magnitude;
        }

        public void ShootProjectile()
        {
            Vector3 bulletSpawn = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            Projectile newProjectile = Instantiate(prefabProjectile, bulletSpawn, Quaternion.identity);

            Vector3 dir = VectorToAttackTarget().normalized;

            newProjectile.Shoot(gameObject, dir);
        }
    }
}