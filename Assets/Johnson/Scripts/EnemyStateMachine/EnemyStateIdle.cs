using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// This class is responsible for the idle state controls ... also this probably should be named pursue... but im lazy
    /// </summary>
    public class EnemyStateIdle : EnemyState
    {
        /// <summary>
        /// Overrides the enemy's update function to use to attack the targeted base
        /// </summary>
        /// <param name="enemy"> stores a reference of the EnemyStateMachine in enemy</param>
        /// <returns>It returns back into itself until the current base is destroyed</returns>
        public override EnemyState Update(EnemyStateMachine enemy)
        {
            if (enemy.goal) // if enemy is not in the goal
            {
                enemy.agent.destination = enemy.goal.transform.position; // sets the enemies destinaation to the current goal

                Vector3[] points = enemy. agent.path.corners; // gets all the points of the enemies current path

                enemy.line.positionCount = points.Length; // adds the gathered points to an array with all the point info

                enemy.line.SetPositions(points); // draws line of current path for testers to see

            }
            else
            {
                enemy.FindClosestGoal(); // find the closest goal
            }
            return null; // stay in current state
        } // end update
    } // end class
} // end namespace