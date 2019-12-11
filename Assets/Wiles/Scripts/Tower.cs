using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class Tower : MonoBehaviour
    {
        public float health = 1996;
        public float attackRange = 8;
        public float attackSpeed = 0.8f;
        public float attackDamage = 77;

        bool isFrozen = false;
        float freezeDuration = 4;

        List<EnemyController> enemies = new List<EnemyController>();
        float atkTimer = 0;
        float frozenTimer = 0;

        MeshRenderer mesh;
        Material defMat;
        public Material frozenMat;

        bool cleanUpEnemyList = false;

        public bool isDead
        {
            get
            {
                return (health <= 0);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            mesh = GetComponent<MeshRenderer>();
            defMat = mesh.materials[0];
        }
        /// <summary>
        /// This function cleans up our enemies array by removing "null"s
        /// due to enemies dying while they are close to this tower.
        /// </summary>
        private void RemoveNullEnemies()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i] == null) enemies.RemoveAt(i);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead) Explode();

            if(cleanUpEnemyList)
            {
                RemoveNullEnemies();
                cleanUpEnemyList = false;
            }

            if (isFrozen)
            {
                mesh.materials[0] = frozenMat;
                frozenTimer += Time.deltaTime;
                if (frozenTimer >= freezeDuration)
                {
                    mesh.materials[0] = defMat;
                    isFrozen = false;
                    frozenTimer = 0;
                }
                return;
            }

            atkTimer += Time.deltaTime;
            if (atkTimer >= attackSpeed && GetClosestEnemy() != null)
            {
                atkTimer = 0;
                Attack(GetClosestEnemy());
            }

        }
        public void Freeze(float duration)
        {
            isFrozen = true;
            freezeDuration = duration;
        }

        void Attack(EnemyController target)
        {
            target.TakeDamage(attackDamage);
        }

        public void StartSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }

        public void EndSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        EnemyController GetClosestEnemy()
        {
            EnemyController result = null;
            float minDis = 0;
            // find closest
            foreach(EnemyController e in enemies)
            {
                if (e == null)
                {
                    cleanUpEnemyList = true;
                    continue; //if this e enemy has already been destroyed but somehow not removed, ignore it.
                }
                float dis = (e.transform.position - transform.position).magnitude; // distance from tower to enemy
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

        void OnTriggerEnter(Collider collider) {
            print("something entered my trigger...");

            EnemyController e = collider.GetComponent<EnemyController>();
            if (e != null) enemies.Add(e);

        }
        void OnTriggerExit(Collider collider) {
            print("something exited my trigger...");


            EnemyController e = collider.GetComponent<EnemyController>();
            if (e != null) enemies.Remove(e);


        }
        public void TakeDamage(float amount)
        {
            health -= amount;
        }
        private void Explode()
        {
            print("ENEMY BOOM");
            // TODO: spawn particles
            // TODO: play audio
            Destroy(gameObject);
        }
    }
}