using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal portalBrother;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (portalBrother.gameObject.transform.position.z == -2)
            {
                other.gameObject.transform.position = portalBrother.gameObject.transform.position + new Vector3(0, 0, 1);
            }
            if (portalBrother.gameObject.transform.position.z == 2)
            {
                other.gameObject.transform.position = portalBrother.gameObject.transform.position + new Vector3(0, 0, -1);
            }
            if (portalBrother.gameObject.transform.position.x == -2)
            {
                other.gameObject.transform.position = portalBrother.gameObject.transform.position + new Vector3(1, 0, 0);
            }
            if (portalBrother.gameObject.transform.position.x == 2)
            {
                other.gameObject.transform.position = portalBrother.gameObject.transform.position + new Vector3(-1, 0, 0);
            }
        }
    }
}
