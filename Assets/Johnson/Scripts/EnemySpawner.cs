﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// This class is used to spawn in an enemy every four seconds
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {

        public GameObject enemy; // holds enemy game object prefab
        public int enemyMaxLimit = 5;
        public int enemyCurrent = 0;
        float timerMin = 0; // timers end point
        float timerStart = 4; // timers start point
        float timerCurrent = 4; // current time
        public bool isSpawnerOn = false; // so user can turn on and off during development

        private void Start()
        {
         
        }

        // Update is called once per frame
        void Update()
        {
            SpawnTimer(); // yay methods
        }

        /// <summary>
        /// This function is an endless timer to keep a constant flow of enemies for testing
        /// </summary>
        private void SpawnTimer()
        {
            // Debug.Log(timerCurrent); // shows current time in the console
            if (isSpawnerOn) // if player has this bool checked do ...
            {
                if (enemyCurrent < enemyMaxLimit)
                {
                    if (timerCurrent <= timerMin) // if the current time is less than 0 do ...
                    {
                        Instantiate(enemy, transform.position, Quaternion.identity); // spawn enemy
                        enemyCurrent += 1;
                        Debug.Log(enemyCurrent);
                        timerCurrent = timerStart; // reset timer
                    }
                    else if (timerCurrent > timerMin) // if current time is greater than 0 do ...
                    {
                        timerCurrent -= 1 * Time.deltaTime; // subtract the time and multiply the number by delta time to get it in seconds
                    }
                }
            }
        }
        
    }
}