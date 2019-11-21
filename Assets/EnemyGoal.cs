using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Webb
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


        // Update is called once per frame
        void Update()
        {

            if (healthBar)
            {
                healthBar.fillAmount = health / 100;
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