using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Johnson
{
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

        // Update is called once per frame
        void Update()
        {
            if (isDead) Explode();
            
        }

        void Explode()
        {
            print("BOOM!");
            // TODO: spawn particles
            // TODO: Play audio
            Destroy(gameObject);
        }

        public void TakeDamage(float amount)
        {
            
            health -= amount;
            healthBar.fillAmount = (health/100);
        }
    }
}