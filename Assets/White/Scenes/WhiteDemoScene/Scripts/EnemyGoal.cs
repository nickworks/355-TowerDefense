using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace White
{
    /// <summary>
    /// This class handles the behavior of the base.
    /// </summary>
    public class EnemyGoal : MonoBehaviour
    {
        /// <summary>
        /// The image that displays the base's health.
        /// </summary>
        public Image healthBar;

        /// <summary>
        /// The amount of health the base has.
        /// </summary>
        float health = 100;

        /// <summary>
        /// Determines if the base is dead.
        /// </summary>
        public bool isDead
        {
            get
            {
                return (health <= 0);
            }
        }

        /// <summary>
        /// Updates the health bar when the base takes damage.
        /// </summary>
        void Update()
        {
            if (healthBar) healthBar.fillAmount = health / 100;

            if (isDead) Explode();
        } // ends the Update() function

        /// <summary>
        /// This function destroys the game object.
        /// </summary>
        private void Explode()
        {
            // TODO: spawn particles
            // TODO: play audio
            Destroy(gameObject);
        } // ends the Explode() function

        /// <summary>
        /// This function takes away the base's health when it takes damage.
        /// </summary>
        /// <param name="amount">The amount of health the base has.</param>
        public void TakeDamage(float amount)
        {
            health -= amount;
        } // ends the TakeDamage() function
    } // ends the EnemyGoal() function
} // ends the White namespace