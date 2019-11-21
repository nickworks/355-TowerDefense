using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class Tower : MonoBehaviour
    {

        List<EnemyController> enemies = new List<EnemyController>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }


        EnemyController GetClosestEnemy()
        {
            EnemyController result = null;
            float minDis = 0;
            // find closest
            
            foreach (EnemyController e in enemies)
            {
                float dis = (e.transform.position - transform.position).magnitude; // distance from tower to enemy
                
                if (dis < minDis || result == null)
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
            EnemyController e = collider.GetComponent<EnemyController>();
            if(e != null)
            {
                enemies.Add(e);
            }
        }
        void OnTriggerExit(Collider collider)
        {
            EnemyController e = collider.GetComponent<EnemyController>();
            if (e != null)
            {
                enemies.Remove(e);
            }
        }
    }
}