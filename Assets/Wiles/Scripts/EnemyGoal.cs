using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wiles
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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (healthBar) healthBar.fillAmount = health / 100;

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