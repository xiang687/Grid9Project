using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImitate : Unit
{
    public emEnemyType enemyType = emEnemyType.Imitate;

    public override void OnStart()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.moveDelegate = OnMoveImitate;
    }
    public void OnMoveImitate(Vector3 direction)
    {
        previousPosition = this.transform.position;
        nextPosition = this.transform.position + direction;
        if (mapManager.wallLocationList.Contains(nextPosition))
        {
            return;
        }
        if ((-1<= nextPosition.x && nextPosition.x <= 1) && (-1 <= nextPosition.z && nextPosition.z <= 1))
        {
            transform.Translate(direction);
        }
        
        Debug.Log(this.transform.position);
    }
}
