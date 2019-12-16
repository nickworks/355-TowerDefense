using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{

    /// <summary>
    /// This class holds all the code for the projectile properties
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public GameObject tower; // get a copy of the tower
        protected GameObject owner; // get a copy of itself
        protected Rigidbody body; // get a copy of the rigidbody attatched tto gameobjec
        EnemyStateMachine enemy; // get a copy of the enemy
        
        public float attackDamage = 25; // attack damage

        float speed = 10; // how fast the projectile can move
                
        float lifetime = 2; // How long the bullet can live for
            
        float age = 0; // how many seconds this projectile has been alive

        /// <summary>
        /// constructor function
        /// </summary>
        void Start()
        {
    
            body = GetComponent<Rigidbody>(); // get the rigidbody on the gameobject
           
        }

        /// <summary>
        /// This is the shoot function for the projectile
        /// </summary>
        /// <param name="owner">this holds a copy of itself</param>
        /// <param name="direction">holds the direction in which to shoot</param>
        public void Shoot(GameObject owner, Vector3 direction)
        {
            
            this.owner = owner; // sets the owner to this class
            body = GetComponent<Rigidbody>(); // get the rigidbody
            body.velocity = direction * speed; // set the velocity of projectile
        }

      

        // Update is called once per frame
        void Update()
        {
            GetOlderAndDie();
        }

        /// <summary>
        /// this function is to prevent memory leaks by destroying unneeded projectiles
        /// </summary>
        protected void GetOlderAndDie()
        {
            age += Time.deltaTime; // increase the age

            if (age >= lifetime) // if age reaches its lifetime
            {
                Destroy(gameObject); // destroy the projectile
            }
        }

        /// <summary>
        /// this function handles when the projectile hits something 
        /// </summary>
        /// <param name="other">holds a copy of the other objects collider</param>
        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject == owner) // if the other collider is the shooter
            {
                return; // don't hit the shooter of this projectile!
            }
            else if (other.gameObject.name == "Tower(Clone)" || other.gameObject.name == "LightningTower(Clone)" || other.gameObject.name == "FreezeTower(Clone)") // ignore all towers, just in case
            {
                return; // dont hit teammates
            } 
            if (other.GetComponent<EnemyStateMachine>() != null) // if the other collider is the enemy
            {
                enemy = other.GetComponent<EnemyStateMachine>(); // get a copy of the enemy
                //print("hit");
                enemy.TakeDamage(attackDamage); // damage it
                Destroy(gameObject); // destroy projectile
            }


        }
    }
}
