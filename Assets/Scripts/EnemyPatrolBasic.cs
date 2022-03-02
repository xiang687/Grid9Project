using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBasic : Unit
{
    public emEnemyType enemyType = emEnemyType.Patrol;

    private int directionFactor = 1;
    public override void OnUpdate()
    {
        timerMove += Time.deltaTime;
        if (timerMove > 0.2f)
        {
            previousPosition = this.transform.position;
            MovePatrol();
            timerMove = 0;
        }
    }

    public void MovePatrol()
    {
        if (this.transform.position.x == -1)
        {
            directionFactor = 1;
        }
        else if (this.transform.position.x == 1)
        {
            directionFactor = -1;
        }
        this.transform.Translate(Vector3.right * directionFactor);
    }
}
