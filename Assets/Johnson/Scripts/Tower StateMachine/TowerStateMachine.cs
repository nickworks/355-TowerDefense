using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{

    /// <summary>
    /// This class is respponsible for handling the tower state machine
    /// </summary>
    public class TowerStateMachine : MonoBehaviour
    {

        TowerState currentState; // the currrent state of the enemy

        public float attackCooldown = 0.5f; // cooldown time for attacks
        public float attackDamage = 25; // attack damage of the tower

        [HideInInspector] // hides public properties from the inspector
        public float timeBetweenShots = .5f; // this holds the time that the boss has to wait before firing again
        [HideInInspector]
        public float timeUntilNextShot = 2; // this hold the time until the next shot

        List<EnemyStateMachine> enemies = new List<EnemyStateMachine>(); // this is a special array that holds all the enemies on scene

        [HideInInspector]
        public float timerAttackCooldown = 0; // current time for the attacks cooldown

        [HideInInspector]
        public Transform attackTarget { get; private set; } // gets the position of the current attack target

        public Projectile prefabProjectile; // holds a projectile prefab

        Vector3 vectorToTarget; // line to target

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (timerAttackCooldown > 0) timerAttackCooldown -= Time.deltaTime; // timer for attack cooldown

            if (currentState == null) SwitchToState(new TowerStateIdle()); // if currentState is empty, then switch to idle state

            if (currentState != null) SwitchToState(currentState.Update(this)); // if currentState is not null, then update the current state
        } // end update

        /// <summary>
        /// this is the start function for the selector mechanic in the game... i broke it and haven't fixed it
        /// </summary>
        public void StartSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.white; // turn white when selected
        }
        /// <summary>
        /// end function for the selector
        /// </summary>
        public void EndSelect()
        {
            GetComponent<MeshRenderer>().material.color = Color.red; // turn back to normal color
        }

        /// <summary>
        /// This function controls the switching of states
        /// </summary>
        /// <param name="newState">Passes in a copy of the state class to be filled with the info from the newState</param>
        private void SwitchToState(TowerState newState)
        {
            if (newState != null) // if newState is not null
            {
                if (currentState != null) currentState.OnEnd(this); // if the currentState is not null, then activate the OnEnd Funtion
                currentState = newState; // currentState then passed the new state
                currentState.OnStart(this); // Activate the OnStart function of the new state
            }
        }

        /// <summary>
        /// This function handles getting the closest enemy to the tower
        /// </summary>
        /// <returns>the closet enemy</returns>
        public EnemyStateMachine GetClosestEnemy()
        {
            EnemyStateMachine result = null; // jolds the end result
            float minDis = 0; //minimum distance
            // find closest

            foreach (EnemyStateMachine e in enemies) 
            {
                float dis = (e.transform.position - transform.position).magnitude; // distance from tower to enemy

                if (dis < minDis || result == null) // if distance is less than minDis or result is null
                {
                    result = e; // result equals current enemy
                    minDis = dis; // minDis equal the distance


                } // end if
            } // end if
            
            return result; // return the end result
        } // end GetClosestEnemy

        /// <summary>
        /// This function activates when something enters the trigger collider
        /// </summary>
        /// <param name="collider">the collider of the object that entered the trigger is passed into the param</param>
        void OnTriggerEnter(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                enemies.Add(e); // add that enemy
                attackTarget = e.transform; // attack target becomes the new enemy

                if(attackTarget != null) SwitchToState(new TowerStateShoot()); // switch state
            }

        }

        /// <summary>
        /// This function activates when something stays in the trigger collider
        /// </summary>
        /// <param name="collider">the collider of the object that entered the trigger is passed into the param</param>
        private void OnTriggerStay(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                attackTarget = e.transform; // attack target becomes the new enemy

                if (attackTarget != null)  SwitchToState(new TowerStateShoot());// switch state
            }
        }

        /// <summary>
        /// This function activates when something exits in the trigger collider
        /// </summary>
        /// <param name="collider">the collider of the object that entered the trigger is passed into the param</param>
        void OnTriggerExit(Collider collider)
        {
            EnemyStateMachine e = collider.GetComponent<EnemyStateMachine>();
            if (e != null)
            {
                enemies.Remove(e); // removes the enemy from the array
                if (attackTarget == null)  SwitchToState(new TowerStateIdle()); // switch state
            }

        }

        /// <summary>
        /// this function handles geting the direction of the current attack target
        /// </summary>
        /// <returns>vector3: vector pointing in the direction of target</returns>
        public Vector3 VectorToAttackTarget()
        {
                return attackTarget.position - new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        }

        /// <summary>
        /// this function handles getting the distance of the vector returned from VectorToAttackTarget
        /// </summary>
        /// <returns>distance from tower to attack target</returns>
        public float DistanceToAttackTarget()
        {
            return VectorToAttackTarget().magnitude;
        }

        /// <summary>
        /// this function handles the spawning and "shooting" of projectile
        /// </summary>
        public void ShootProjectile()
        {
            Vector3 bulletSpawn = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z); // gets the spawn point for bullet
            Projectile newProjectile = Instantiate(prefabProjectile, bulletSpawn, Quaternion.identity); // holds info to spawn the bullet into the scene

            Vector3 dir = VectorToAttackTarget().normalized; // gets the direction to the attack target

            newProjectile.Shoot(gameObject, dir); // spawn bullet
        }
    }
}