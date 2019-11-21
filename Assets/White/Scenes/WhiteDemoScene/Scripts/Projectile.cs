using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace White
{
    public class Projectile : MonoBehaviour
    {
        protected GameObject owner;
        protected Rigidbody body;

        float speed = 10;
        float lifetime = 2;
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

        void Update()
        {
            GetOlderAndDie();
        }

        protected void GetOlderAndDie()
        {
            age += Time.deltaTime;
            if (age >= lifetime) Destroy(gameObject);
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject == owner) return;

            Destroy(gameObject);
        }
    }
}