using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wiles
{
    public class EnemyGoal : MonoBehaviour
    {
        public float health = 3327;
        public float attackRange = 7.5f;
        public float attackSpeed = 1;
        public float attackDamage = 74;

        public Image healthBar;

        float maxHealth;

        public bool isDead
        {
            get
            {
                return (health <= 0);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            maxHealth = health;
        }

        // Update is called once per frame
        void Update()
        {
            if (healthBar) healthBar.fillAmount = health / maxHealth;

            if (isDead) Explode();
        }

        private void Explode()
        {
            print("BOOM");
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