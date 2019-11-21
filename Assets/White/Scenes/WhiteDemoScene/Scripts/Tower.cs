using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace White
{
    public class Tower : MonoBehaviour
    {
        List<EnemyController> enemies = new List<EnemyController>();

        public Projectile prefabProjectile;

        void Start()
        {

        }
        
        void Update()
        {

        }

        EnemyController GetClosestEnemy()
        {
            EnemyController result = null;
            float minDis = 0;

            // find closest
            foreach(EnemyController e in enemies)
            {
                float dis = (e.transform.position - transform.position).magnitude; // distance from tower to enemy
                if (dis < minDis)
                {
                        result = e;
                        minDis = dis;
                }
            }

            return result;
        }

        EnemyController GetRandomEnemy()
        {
            if (enemies.Count <= 0) return null;

            int index = Random.Range(0, enemies.Count);

            return enemies[index];
        }

        void OnTriggerEnter(Collider collider)
        {
            print("something entered my trigger");
            EnemyController e = collider.GetComponent<EnemyController>();
            if (e != null) enemies.Add(e);
        }

        void OnTriggerExit(Collider collider)
        {
            print("something exited my trigger");
            EnemyController e = collider.GetComponent<EnemyController>();
            if (e != null) enemies.Remove(e);
        }
    }
}
