using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class FreezeAttack : MonoBehaviour
    {
        public float freezeDuration = 4;
        public float areaDamage = 114;
        public float areaDamageTower = 46; //How much damage are dealt to towers. Might not implament.
        public float range = 6;
        public float freezeRange = 3;
        public float cooldownTime = 16;

        /*
         * This script needs access to it's owner script, either an EnemyController or Tower, to determine which friendly or not.
         * [should maybe change it to TowerController]
         * This script needs access to any gameobjects w/in it's freeze attack range, 
         * tell which of them are non-friendly, then damage and freeze them.
         */

        GameObject owner;
        EnemyController ec;
        Tower t;
        bool isPlayerFirendly;

        List<EnemyController> enemyControllers = new List<EnemyController>();
        List<Tower> towers = new List<Tower>();

        bool atkMode = false;
        float atkTimer = 15;

        // Start is called before the first frame update
        void Start()
        {
            ec = GetComponent<EnemyController>();
            t = GetComponent<Tower>();

            if (ec == null && t == null)  //if EnemyController and Tower are both null,
            {
                Destroy(gameObject); //kill itself, it has no valid owner.
            }
            if (ec != null && t != null) { //if EnemyController and Tower are both valid,
                Destroy(gameObject); //kill itself, it cannot have two owners.
            }

            if (ec != null)
            {
                owner = ec.gameObject;
                isPlayerFirendly = false;
            } else if (t != null)
            {
                owner = t.gameObject;
                isPlayerFirendly = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            atkTimer += Time.deltaTime;

            if (atkTimer >= cooldownTime)
            {
                print("TIME TO FREEZE!");
                atkTimer = cooldownTime;
                if (isPlayerFirendly)
                {

                }
                else
                {
                    GetClosestTower();
                    if (atkMode)
                    {
                        FreezeAllTowersInRange();
                        atkTimer = 0;
                    }
                }
            }
           
        }

        void FreezeAllTowersInRange()
        {
            foreach (Tower t in towers)
            {
                float dis = (t.transform.position - transform.position).magnitude; // distance from tower to enemy
                if (dis < range)
                {
                    t.TakeDamage(areaDamageTower);
                    t.Freeze(freezeDuration);
                }
            }
        }

        EnemyController GetClosestEnemyController()
        {
            EnemyController result = null;
            float minDis = 0;
            // find closest
            foreach (EnemyController e in enemyControllers)
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

        Tower GetClosestTower()
        {
            Tower result = null;
            float minDis = 0;
            // find closest
            foreach (Tower t in towers)
            {
                float dis = (t.transform.position - transform.position).magnitude; // distance from tower to enemy
                if (dis < minDis || result == null)
                {
                    result = t;
                    minDis = dis;
                }
            }
            if (minDis <= freezeRange) atkMode = true;
            return result;
        }

        void OnTriggerEnter(Collider collider)
        {
            print("something entered my trigger...");

            EnemyController e = collider.GetComponent<EnemyController>();
            if (e != null) enemyControllers.Add(e);

            Tower t = collider.GetComponent<Tower>();
            if (t != null) towers.Add(t);

        }
        void OnTriggerExit(Collider collider)
        {
            print("something exited my trigger...");


            EnemyController e = collider.GetComponent<EnemyController>();
            if (e != null) enemyControllers.Remove(e);

            Tower t = collider.GetComponent<Tower>();
            if (t != null) towers.Remove(t);
        }
    }
}