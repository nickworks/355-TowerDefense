using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public GameObject tower;
        protected GameObject owner;
        protected Rigidbody body;
        EnemyStateMachine enemy;
        
        public float attackDamage = 25;

        float speed = 10;
        /// <summary>
        /// How long the bullet can live for.
        /// </summary>
        float lifetime = 2;

        /// <summary>
        /// how many seconds this projectile has been alive
        /// </summary>
        float age = 0;

        void Start()
        {
            body = GetComponent<Rigidbody>();
           
        }

        public void Shoot(GameObject owner, Vector3 direction)
        {
            
            this.owner = owner;
            body = GetComponent<Rigidbody>();
            body.velocity = direction * speed;
        }

      

        // Update is called once per frame
        void Update()
        {
            GetOlderAndDie();
        }

        protected void GetOlderAndDie()
        {
            age += Time.deltaTime;

            if (age >= lifetime)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject == owner)
            {
                return; // don't hit the shooter of this projectile!
            }
            else if (other.gameObject.name == "Tower(Clone)")
            {
                return;
            }
            if (other.GetComponent<EnemyStateMachine>() != null)
            {
                enemy = other.GetComponent<EnemyStateMachine>();
                //print("hit");
                enemy.TakeDamage(attackDamage);
                Destroy(gameObject);
            }


        }
    }
}
