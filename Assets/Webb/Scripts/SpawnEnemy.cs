using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{
    /// <summary>
    /// spawns the enemy every  1 to 3 seconds 
    /// </summary>
    public class SpawnEnemy : MonoBehaviour
    {
        // Start is called before the first frame update
         float spawnCoolDown;// how long before next spawn
        Vector3 spawnPos; // where to spawn
        public GameObject enemy;// gets prefab for instaniate
        /// <summary>
        /// sets postion of the object location
        /// </summary>
        void Start()
        {
            spawnPos = gameObject.transform.position;
        }

        // Update is called once per frame
        /// <summary>
        /// spwns enenmy
        /// </summary>
        void Update()
        {
            SpawnE();
        }
        /// <summary>
        ///  spawns the enemy every  1 to 3 seconds 
        /// </summary>
        public void SpawnE()
        {
            spawnCoolDown -= Time.deltaTime;
            if (spawnCoolDown <= 0)
            {
                Instantiate(enemy, spawnPos, Quaternion.identity);
                spawnCoolDown = Random.Range(1, 4);
            }
        }
    }
}
