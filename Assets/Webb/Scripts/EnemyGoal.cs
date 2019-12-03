using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Webb
{
    public class EnemyGoal : MonoBehaviour
    {
        public Image healthBar;
        float health = 5000;
        public bool isDead
        {
            get
            {
                return (health <= 0);
            }
        }


        // Update is called once per frame
        void Update()
        {

            if (healthBar)
            {
                healthBar.fillAmount = health / 5000;
            }
            if (isDead) Explode();
        }
        void Explode()
        {
            print("Boom");
            //toDo: spawn particles and sound
            Destroy(gameObject);
        }
        public void TakeDamge(float amount)
        {
            health -= amount;
        }
    }
}