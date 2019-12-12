using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace White
{
    /// <summary>
    /// This class defines the behavior of the towers.
    /// </summary>
    public class Tower : MonoBehaviour
    {
        /// <summary>
        /// This list stores the enemies seen by the tower.
        /// </summary>
        List<EnemyController> enemies = new List<EnemyController>();

        /// <summary>
        /// The prefab that is being used as a projectile.
        /// </summary>
        public Projectile prefabProjectile;

        /// <summary>
        /// The target that the tower is currently attacking.
        /// </summary>
        public Transform attackTarget { get; private set; }
        
        /// <summary>
        /// This function updates each tower spawned.
        /// </summary>
        void Update()
        {
            GetClosestEnemy();
        } // ends the Update() function

        /// <summary>
        /// This function finds the enemy closest to the tower.
        /// </summary>
        /// <returns>The enemy closest to the tower.</returns>
        EnemyController GetClosestEnemy()
        {
            /// <summary>
            /// Which enemy is the closest to the tower.
            /// </summary>
            EnemyController result = null;

            /// <summary>
            /// The minimum distance the enemy can be from the tower.
            /// </summary>
            float minDis = 0;

            // find closest
            foreach(EnemyController e in enemies)
            {
                /// <summary>
                /// The distance from the tower to the enemy.
                /// </summary>
                float dis = (e.transform.position - transform.position).magnitude;

                if (dis < minDis)
                {
                        result = e;
                        minDis = dis;
                } 

                ShootProjectile();
            }

            return result;
        } // ends the GetClosestEnemy() function

        /// <summary>
        /// This function makes the tower choose a random enemy to fire at.
        /// </summary>
        /// <returns>The enemy to fire at.</returns>
        EnemyController GetRandomEnemy()
        {
            if (enemies.Count <= 0) return null;

            /// <summary>
            /// The enemy to fire at.
            /// <summary>
            int index = UnityEngine.Random.Range(0, enemies.Count);

            return enemies[index];
        } // ends the GetRandomEnemy() function

        /// <summary>
        /// This function determines if an enemy has entered the tower's trigger.
        /// </summary>
        /// <param name="collider">The tower's collider.</param>
        void OnTriggerEnter(Collider collider)
        {
            //print("something entered my trigger");

            /// <summary>
            /// The enemy that can enter the tower's trigger.
            /// </summary>
            EnemyController e = collider.GetComponent<EnemyController>();

            if (e != null) enemies.Add(e);
        } // ends the OnTriggerEnter() function

        /// <summary>
        /// This function determines if the enemy has left the tower's trigger.
        /// </summary>
        /// <param name="collider">The tower's collider.</param>
        void OnTriggerExit(Collider collider)
        {
            //print("something exited my trigger");

            /// <summary>
            /// The enemy that can exit the tower's trigger.
            /// </summary>
            EnemyController e = collider.GetComponent<EnemyController>();

            if (e != null) enemies.Remove(e);
        } // ends the OnTriggerExit() function

        /// <summary>
        /// This function determines the vector from the tower to the attack target.
        /// </summary>
        /// <returns>The vector from the tower to the attack target.</returns>
        public Vector3 VectorToAttackTarget()
        {
            return attackTarget.position - transform.position;
        } // ends the VectorToAttackTarget()

        /// <summary>
        /// This function makes the tower shoot a projectile at the enemy.
        /// </summary>
        private void ShootProjectile()
        {
            /// <summary>
            /// Instantiates the projectile.
            /// </summary>
            Projectile newProjectile = Instantiate(prefabProjectile, transform.position, Quaternion.identity);

            /// <summary>
            /// The direction for the tower to shoot the projectile.
            /// </summary>
            Vector3 dir = (VectorToAttackTarget() + UnityEngine.Random.insideUnitSphere * 5).normalized;

            newProjectile.Shoot(gameObject, dir);
        } // ends the ShootProjectile() function
    } // ends the Tower class
} // ends the White namespace