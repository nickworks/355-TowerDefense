using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Webb
{
    /// <summary>
    /// sets up to keep track of goals and health and destroy upon hitting zero
    /// </summary>
    public class EnemyGoal : MonoBehaviour
    {
        public Image healthBar; // gets refrenc to health bar
        float health = 5000; // current health
        public bool isDead // tells when to destroy this object
        {
            get
            {
                return (health <= 0);
            }
        }

        /// <summary>
        /// updates the health bar
        /// kills obejct when isdead is true
        /// </summary>
        // Update is called once per frame
        void Update()
        {

            if (healthBar)
            {
                healthBar.fillAmount = health / 5000;
            }
            if (isDead) Explode();
        }/// <summary>
        /// destroys this gameobject
        /// </summary>
        void Explode()
        {
            print("Boom");
            //toDo: spawn particles and sound
            Destroy(gameObject);
        }
        /// <summary>
        /// cause the goal to take damge
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamge(float amount)
        {
            health -= amount;
        }
    }
}