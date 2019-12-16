using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wiles
{
    public class TowerController : MonoBehaviour
    {
        public float health = 1996;
        public float attackRange = 8;
        public float attackSpeed = 0.8f;
        public float attackDamage = 77;

        public bool justFrozen = false;
        public bool isFrozen = false;
        public float freezeDuration = 4;

        float freezeAtkDurration = 4;

        List<EnemyController> enemies = new List<EnemyController>();
        public float atkTimer = 0;
        public float frozenTimer = 0;

        public MeshRenderer mesh;
        public Material defMat;
        public Material frozenMat;

        bool cleanUpEnemyList = false;

        TowerState currentState;
        public TowerState previousState;

        public enum AttackType {Zap, Projectile, Ice};

        public AttackType currentAttack;

        public Projectile projectile;

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

            if (cleanUpEnemyList)
            {
                RemoveNullEnemies();
                cleanUpEnemyList = false;
            }

            if (currentState == null) SwitchToState(new TowerStateIdle());
            if (currentState != null) SwitchToState(currentState.Update(this));

            if (justFrozen)
            {
                justFrozen = false;
                SwitchToState(new TowerStateFrozen());
            }
        }
        private void SwitchToState(TowerState newState)
        {
            if (newState != null)
            {
                if (currentState != null) currentState.OnEnd(this);
                previousState = currentState;
                currentState = newState;
                currentState.OnStart(this);
            }
        }
        public void Freeze(float duration)
        {
            if (!isFrozen)
            {
                justFrozen = true;
                isFrozen = true;
                freezeDuration = duration;
            }
        }

        public bool Zap(EnemyController target)
        {
            if (target == null) return false;
            target.TakeDamage(attackDamage);
            return true;
        }

        public bool Shoot(EnemyController target)
        {
            if (target == null) return false;
            Instantiate(projectile, (transform.position + new Vector3(0, 1, 0)), Quaternion.identity);
            projectile.Shoot(gameObject, target.transform.position, attackDamage);
            return true;
        }

        public bool Freeze(EnemyController target)
        {
            if (target == null) return false;
            target.Freeze(freezeAtkDurration);
            return true;
        }

        public void StartSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }

        public void EndSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public EnemyController GetClosestEnemy()
        {
            EnemyController result = null;
            float minDis = 0;
            // find closest
            foreach (EnemyController e in enemies)
            {
                if (e == null)
                {
                    cleanUpEnemyList = true;
                    continue; //if this e enemy has already been destroyed but somehow not removed, ignore it.
                }
                float dis = (e.transform.position - transform.position).magnitude; // distance from tower to enemy
                if (dis < minDis || result == null)
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
            print("something entered my trigger...");

            EnemyController e = collider.GetComponent<EnemyController>();
            if (e != null) enemies.Add(e);

        }
        void OnTriggerExit(Collider collider)
        {
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