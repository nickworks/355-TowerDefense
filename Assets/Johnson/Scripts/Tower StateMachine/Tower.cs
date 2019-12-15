using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    public class Tower : MonoBehaviour
    {
        public float attackCooldown = 0.5f;
        public float attackDamage = 25;

        float timeBetweenShots = .5f; // this holds the time that the boss has to wait before firing again
        float timeUntilNextShot = 2; // this hold the time until the next shot

        List<EnemyStateMachine> enemies = new List<EnemyStateMachine>();
        float timerAttackCooldown = 0;

        [HideInInspector]
        public Transform attackTarget { get; private set; } 

        public Projectile prefabProjectile;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;
            


        }

        public void StartSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        public void EndSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        EnemyStateMachine GetClosestEnemy()
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
            if(e != null)
            {
                attackTarget = e.transform;
                enemies.Add(e);

                timeUntilNextShot -= Time.deltaTime;

                if (timeUntilNextShot <= 0)
                {
                    ShootProjectile();
                    timeUntilNextShot = timeBetweenShots;
                }
            }

        }
        private void OnTriggerStay(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                attackTarget = e.transform;

                timeUntilNextShot -= Time.deltaTime;

                if (timeUntilNextShot <= 0)
                {
                    ShootProjectile();
                    timeUntilNextShot = timeBetweenShots;
                }
            }
        }
        void OnTriggerExit(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                enemies.Remove(e);

                timeUntilNextShot -= Time.deltaTime;

                if (timeUntilNextShot <= 0)
                {
                    ShootProjectile();
                    timeUntilNextShot = timeBetweenShots;
                }
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

        

    } // end class
}// end namespace