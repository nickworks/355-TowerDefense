﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Wiles
{
    public class EnemyController : MonoBehaviour
    {
        public float health = 250;
        public float attackDamage = 25;
        public float attackRange = 0;
        public float attackCooldown = 0.5f;
        public float speed = 2.5f;

        public Transform gui;
        public Image healthBar;

        NavMeshAgent agent;
        LineRenderer line;
        EnemyGoal goal;
        bool isAttackState = false;
        private float timerAttackCooldown = 0;
        float maxHealth;

        public bool justFrozen = false;
        public bool isFrozen = false;
        public float freezeDuration = 4;

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

            agent = GetComponent<NavMeshAgent>();
            line = GetComponent<LineRenderer>();
            maxHealth = health;

        }

        void LateUpdate()
        {
            gui.rotation = Quaternion.Euler(60, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (healthBar) healthBar.fillAmount = health / maxHealth;

            if (isDead) Explode();

            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime;

            if (isAttackState)
            {

                if (goal)
                {

                    if(timerAttackCooldown <= 0)
                    {
                        goal.TakeDamage(attackDamage);
                        timerAttackCooldown = attackCooldown;
                    }

                } else
                {
                    isAttackState = false;
                }
            }

            if(goal)
            {
                
                //agent.ResetPath();
                agent.destination = goal.transform.position;

                /*
                Vector3[] points = agent.path.corners;

                line.positionCount = points.Length;

                line.SetPositions(points);
                */

            } else
            {
                FindClosestGoal();

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

        private void Explode()
        {
            print("ENEMY BOOM");
            // TODO: spawn particles
            // TODO: play audio
            Destroy(gameObject);
        }

        void FindClosestGoal()
        {
            EnemyGoal[] goals = GameObject.FindObjectsOfType<EnemyGoal>();

            float minDis = 0;
            foreach (EnemyGoal g in goals)
            {
                float dis = (g.transform.position - transform.position).magnitude; // distance to EnemyGoal g

                if (dis < minDis || goal == null)
                {
                    goal = g;
                    minDis = dis;
                }

            }
        }


        void OnTriggerEnter(Collider trigger)
        {
            //print("enemy entered a trigger volume");

            if(trigger.GetComponent<EnemyGoal>() != null)
            {
                isAttackState = true;
            }
            /*
            Tower tower = trigger.GetComponent<Tower>();
            if(tower != null)
            {
                print("enemy close to a tower");
            }*/
        }
        void OnTriggerExit(Collider trigger)
        {
            //print("enemy exited a trigger volume");

            if (trigger.GetComponent<EnemyGoal>() != null)
            {
                isAttackState = false;
            }

        }
        public void TakeDamage(float amount)
        {
            print("TAKE A DAMAGE!");
            health -= amount;

        }
    }
}