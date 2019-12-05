using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb {
    public class Tower : MonoBehaviour
    {

        public GameObject prefabDarkPostion;
        Vector3 spawnPos;
        Vector3 spawnPosYIncrease;
        float timer;
        public float coolDown = .5f;
        
        List<EnemyController> enemies = new List<EnemyController>();
        public static Vector3 enemyPostion;
        // Start is called before the first frame update
        void Start()
        {
            spawnPosYIncrease = new Vector3(0, 4, -1);
             spawnPos = gameObject.transform.position + spawnPosYIncrease;

        }
        public void StartSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        public void EndSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        // Update is called once per frame
        void Update()
        {
      GetClosetEnemy();
            RemoveNullEnemies();



        }
           EnemyController GetClosetEnemy()
            {
               EnemyController result = null;
                //find ccloseset
                float minDis = 0;

                foreach(EnemyController e in enemies)
                {
                if (e == null) continue;
                    float dis = (e.transform.position - transform.position).magnitude;
                    if(dis < minDis || result == null)
                    {
                        result = e;
                        minDis = dis; 
                    enemyPostion = (result.transform.position);
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        Instantiate(prefabDarkPostion, spawnPos, Quaternion.identity);
                        timer = coolDown;
                    }
                  
                }
                }
                return result;
            }  
        public void RemoveNullEnemies()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i] == null) enemies.RemoveAt(i);
            }
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