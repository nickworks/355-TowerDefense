using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb {
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
           EnemyController GetClosetEnemy()
            {
                EnemyController result = null;
                //find ccloseset
                float minDis = 0;

                foreach(EnemyController e in enemies)
                {
                    float dis = (e.transform.position - transform.position).magnitude;
                    if(dis < minDis || result == null)
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
            EnemyController  e = collider.GetComponent<EnemyController>();
            if (e != null) enemies.Add(e);
            print("u enter");
        }
        void OnTriggerExit(Collider collider)
        {
            EnemyController e = collider.GetComponent<EnemyController>();
            if (e != null) enemies.Remove(e);
            print("u leave");
        }
    }
}