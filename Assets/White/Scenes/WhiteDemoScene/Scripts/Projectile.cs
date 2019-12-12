using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace White
{
    /// <summary>
    /// This class determines the behavior of the projectiles.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        /// <summary>
        /// The tower that fired the projectile.
        /// </summary>
        protected GameObject owner;

        /// <summary>
        /// The object that represents the projectile.
        /// </summary>
        protected Rigidbody body;

        /// <summary>
        /// The speed of projectiles.
        /// </summary>
        float speed = 10;

        /// <summary>
        /// How long the projectiles live after being fired.
        /// </summary>
        float lifetime = 2;

        /// <summary>
        /// The age of the projectiles.
        /// </summary>
        float age = 0;
   
        /// <summary>
        /// This function sets up the projectile.
        /// </summary>
        void Start()
        {
            body = GetComponent<Rigidbody>();
        } // ends the Start() function

        /// <summary>
        /// This function enables the tower to shoot projectiles.
        /// </summary>
        /// <param name="owner">The tower that shot the projectile.</param> 
        /// <param name="direction">The direction that the projectile is firing in.</param>
        public void Shoot(GameObject owner, Vector3 direction)
        {
            this.owner = owner;
            body = GetComponent<Rigidbody>();
            body.velocity = direction * speed;
        } // ends the Shoot() function

        /// <summary>
        /// This function updates the projectiles.
        /// </summary>
        void Update()
        {
            GetOlderAndDie();
        } // ends the Update() function

        /// <summary>
        /// This function destroys the projectile after a length of time if it doesn't hit its target.
        /// </summary>
        protected void GetOlderAndDie()
        {
            age += Time.deltaTime;
            if (age >= lifetime) Destroy(gameObject);
        } // ends the GetOlderAndDie() function

        /// <summary>
        /// This function destroys the projectile if it hits its target.
        /// </summary>
        /// <param name="collider"></param> // The collider that must be hit for the projectile to be destroyed.
        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject == owner) return;

            Destroy(gameObject);
        } // ends the OnTriggerEnter() function
    } // ends the Projectile class
} // ends the White namespace