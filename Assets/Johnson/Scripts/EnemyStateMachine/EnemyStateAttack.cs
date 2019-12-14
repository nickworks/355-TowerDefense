using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This keeps all the code within the brackets inside this johnson namespace. also other classes must be inside the same namespace to access any other classes code inside the namespace
/// </summary>
namespace Johnson
{
    /// <summary>
    /// This class/State handles when the enemies die
    /// </summary>
    public class EnemyStateAttack : EnemyState
    {

        /// <summary>
        /// Overrides the enemy's update function to use to attack the targeted base
        /// </summary>
        /// <param name="enemy"> stores a reference of the EnemyStateMachine in enemy</param>
        /// <returns>It returns back into itself until the current base is destroyed</returns>
        public override EnemyState Update(EnemyStateMachine enemy)
        {
            if (enemy.goal)
            {
                if (enemy.timerAttackCooldown <= 0)
                {
                    enemy.goal.TakeDamage(enemy.attackDamage);
                    enemy.timerAttackCooldown = enemy.attackCooldown;

                }
            }
           
            return null;
        }
    }
}