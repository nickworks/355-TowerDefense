using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class EnemyWaveHandler : MonoBehaviour
    {

        public List<GameObject> spawnables;

        int currentRound = 0;
        List<GameObject> aliveSpawns = new List<GameObject>();
        bool cleanUpList = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (aliveSpawns.Count <= 0)
            {
                currentRound++;
                spawnNextWave();
            }
            else
            {
                foreach (GameObject e in aliveSpawns)
                {
                    if (e == null)
                    {
                        cleanUpList = true;
                    }
                }
                if (cleanUpList) RemoveNullObjects();
            }
        }

        /// <summary>
        /// This function cleans up our aliveSpawns list by removing "null"s.
        /// </summary>
        private void RemoveNullObjects()
        {
            for (int i = aliveSpawns.Count - 1; i >= 0; i--)
            {
                if (aliveSpawns[i] == null) aliveSpawns.RemoveAt(i);
            }
        }

        void spawnNextWave()
        {
            for (int j = currentRound; j >= 0; j--)
            {
                GameObject spawn = Instantiate(spawnables[0], gameObject.transform.position, Quaternion.identity);
                aliveSpawns.Add(spawn);
            }
        }
    }
}