using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace White
{
    public class EnemyGoal : MonoBehaviour
    {
        public Image healthBar;

        float health = 100;
        public bool isDead
        {
            get
            {
                return (health <= 0);
            }
        }

        void Update()
        {
            if (healthBar) healthBar.fillAmount = health / 100;

            if (isDead) Explode();
        }

        private void Explode()
        {
            // TODO: spawn particles
            // TODO: play audio
            Destroy(gameObject);
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
        }
    }
}