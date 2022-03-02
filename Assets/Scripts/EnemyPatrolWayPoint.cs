using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolWayPoint : MonoBehaviour
{
    public Transform[] WayPoints;

    float moveSpeed = 2f;
    Vector3 direction;
    int nextIndex = 0;

    // Update is called once per frame
    void Update()
    {
        direction = (WayPoints[nextIndex].position - this.transform.position).normalized;
        this.transform.Translate(direction * moveSpeed * Time.deltaTime);
        if (Vector3.Distance(this.transform.position, WayPoints[nextIndex].position)<0.01f)
        {
            this.transform.position = WayPoints[nextIndex].position;
            nextIndex++;
            if (nextIndex == WayPoints.Length)
            {
                nextIndex = 0;
            }
        }
    }
}
