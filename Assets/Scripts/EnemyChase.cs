using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : Unit
{
    public emEnemyType enemyType = emEnemyType.Chase;
    public override void OnStart()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
    }
    public override void OnUpdate()
    {
        MoveChase();
    }

    public void MoveChase()
    {
        // use waypoints
    }
}
