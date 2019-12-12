using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace White
{
    /// <summary>
    /// This class spawns a new enemy.
    /// </summary>
    public class SpawnEnemy : MonoBehaviour
    {
        /// <summary>
        /// The object that spawns an enemy.
        /// </summary>
        public GameObject objectType;

        /// <summary>
        /// Whether or not the spawner needs to stop spawning enemies.
        /// </summary>
        public bool stopSpawning = false;

        /// <summary>
        /// The amount of time before the enemy is spawned.
        /// </summary>
        public float spawnTime;

        /// <summary>
        /// The amount of time between spawns.
        /// </summary>
        public float spawnDelay;

        /// <summary>
        /// Sets up the spawner.
        /// </summary>
        void Start()
        {
            InvokeRepeating("SpawnAnEnemy", spawnTime, spawnDelay);
        } // ends the Start() function

        /// <summary>
        /// Updates the spawner to keep spawning enemies.
        /// </summary>
        public void SpawnAnEnemy()
        {
            Instantiate(objectType, transform.position, transform.rotation);
            if (stopSpawning)
            {
                CancelInvoke("SpawnBumper");
            }
        } // ends the SpawnAnEnemy() function
    } // ends the SpawnEnemy class
} // ends the White namespace