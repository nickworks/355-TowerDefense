using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {

        protected GameObject owner;
        protected Rigidbody body;

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

            if (other.gameObject == owner) return; // don't hit the shooter of this projectile!

            print("hit");
            Destroy(gameObject);
        }
    }
}
