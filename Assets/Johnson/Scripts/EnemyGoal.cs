using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// This class handles the base functions
    /// </summary>
    public class EnemyGoal : MonoBehaviour
    {
        [HideInInspector]
        public float health = 100;
        public Image healthBar;

        public bool isDead // is a property not a field, so it wont show up in editor
        {
            get
            {
                return (health <= 0);
            }
        }

        /// <summary>
        /// update is called once per frame
        /// </summary>
        void Update()
        {
            if (isDead) Explode();
            
        }

        /// <summary>
        /// this handles the death animation for the base
        /// </summary>
        void Explode()
        {
            print("BOOM!");
            // TODO: spawn particles
            // TODO: Play audio
            Destroy(gameObject);
        }

        /// <summary>
        /// This function handles updating health GUI for any incoming damage
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(float amount)
        {
            health -= amount;
            healthBar.fillAmount = (health/100);
        }
    }
}