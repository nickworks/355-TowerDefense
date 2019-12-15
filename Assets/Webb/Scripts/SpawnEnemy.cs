using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb
{
    public class SpawnEnemy : MonoBehaviour
    {
        // Start is called before the first frame update
         float spawnCoolDown;
        Vector3 spawnPos;
        public GameObject enemy;
        void Start()
        {
            spawnPos = gameObject.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            SpawnE();
        }
      public  void SpawnE()
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
