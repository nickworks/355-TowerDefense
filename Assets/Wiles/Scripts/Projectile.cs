using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        protected GameObject owner;
        protected Rigidbody body;


        Vector3 attackTarget;

        public float speed = 50;

        float damage = 1;
        /// <summary>
        ///  How many seconds this projectile should have
        /// </summary>
        float lifetime = 2;
        /// <summary>
        /// How many seconds this projectile has been alive.
        /// </summary>
        float age = 0;
        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody>();
        }

        public void Shoot(GameObject owner, Vector3 direction, float damage)
        {
            this.owner = owner;

            body = GetComponent<Rigidbody>();
            body.velocity = direction * speed;

            this.damage = damage;
        }

        // Update is called once per frame
        void Update()
        {
            GetOlderAndDie();
        }

        protected void GetOlderAndDie()
        {
            age += Time.deltaTime;

            if (age >= lifetime) Destroy(gameObject);
        }

        public void OnTriggerEnter(Collider collider)
        {

            EnemyController e = collider.gameObject.GetComponent<EnemyController>();

            if (e != null)
            {
                e.TakeDamage(damage);
                Destroy(gameObject);
            }

        }
    }
}