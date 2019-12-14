using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Johnson
{
    public class EnemyStateIdle : EnemyState
    {
        public override EnemyState Update(EnemyStateMachine enemy)
        {
            if (enemy.goal)
            {
                enemy.agent.destination = enemy.goal.transform.position;

                Vector3[] points = enemy. agent.path.corners;

                enemy.line.positionCount = points.Length;

                enemy.line.SetPositions(points);

            }
            else
            {
                enemy.FindClosestGoal();
            }
            return null;
        }
    }
}