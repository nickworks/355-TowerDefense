using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{/// <summary>
/// 
/// </summary>
    public class Tower : MonoBehaviour
    {

        public GameObject prefabDarkPostion;// bullets to spawn
        public Material tower;// gets metrial for tower
        public Material clicked;//material for when tower is clicke
        Vector3 spawnPos;// where to spawn
        Vector3 spawnPosYIncrease;//where y postion is
        float timer = 0; // keeps track of time
        public float coolDown = .5f;// 

        List<EnemyController1> enemies = new List<EnemyController1>();//enemies are kept in an array
        public static Vector3 enemyPostion;//kepps track of enmies loacation
        // Start is called before the first frame update
        /// <summary>
        /// stes up spawn pos and spawnyincreas
        /// </summary>
        void Start()
        {
            spawnPosYIncrease = new Vector3(0, 4, -1);
            spawnPos = gameObject.transform.position + spawnPosYIncrease;

        }
        /// <summary>
        /// changes material when clicked
        /// </summary>
        public void StartSelect()
        {
            GetComponent<MeshRenderer>().material = clicked;
        }
        /// <summary>
        /// changes matrial back when not selcted
        /// </summary>
        public void EndSelect()
        {
            GetComponent<MeshRenderer>().material = tower;
        }
        // Update is called once per frame
        /// <summary>
        /// calls two functions get cloestenemy
        /// removenullenimes
        /// </summary>
        void Update()
        {
            GetClosetEnemy();
            RemoveNullEnemies();




        }
        /// <summary>
        /// looks for closet enmy in array
        /// then spans a projtile
        /// </summary>
        /// <returns></returns>
        EnemyController1 GetClosetEnemy()
        {
            EnemyController1 result = null;
            //find ccloseset
            float minDis = 0;

            foreach (EnemyController1 e in enemies)
            {
                if (e == null) continue;
                float dis = (e.transform.position - transform.position).magnitude;
                if (dis < minDis || result == null)
                {
                    result = e;
                    minDis = dis;
                    enemyPostion = (result.transform.position);
                    timer -= Time.deltaTime;
                    if (timer <= 0 )
                    {
                        Instantiate(prefabDarkPostion, spawnPos, Quaternion.identity);
                        print("help");
                        timer = coolDown;
                    }
                   
                }
            }
            return result;
        }
        /// <summary>
        /// removes dead enmies fromarray
        /// </summary>
        public void RemoveNullEnemies()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i] == null) enemies.RemoveAt(i);
            }
        }/// <summary>
        /// used to get a random enemy from the arry instead of the closest
        /// </summary>
        /// <returns></returns>
        EnemyController1 GetRandomEnemy()
        {
            if (enemies.Count <= 0) return null;
            int index = Random.Range(0, enemies.Count);
            return enemies[index];
        }
        /// <summary>
        /// if enenmy has enemycontroller1 it gets added to the array
        /// </summary>
        /// <param name="collider"></param>
        void OnTriggerEnter(Collider collider)
        {
            EnemyController1 e = collider.GetComponent<EnemyController1>();
            if (e != null) enemies.Add(e);
            print("u enter");
        }
        /// <summary>
        /// if enenmy has enemycontroller1 when it exits trigger it get removeds from array
        /// </summary>
        /// <param name="collider"></param>
        void OnTriggerExit(Collider collider)
        {
            EnemyController1 e = collider.GetComponent<EnemyController1>();
            if (e != null) enemies.Remove(e);
            print("u leave");
        }
    }
}