using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintFloorColor : MonoBehaviour
{
    void Update()
    {
        DetectAndPaint();
    }

    void DetectAndPaint()
    {
        Ray ray = new Ray(transform.position, transform.up * 100);          

        Debug.DrawLine(transform.position, transform.position + transform.forward * 100, Color.yellow);  
        RaycastHit hitInfo;                                 
        if (Physics.Raycast(ray, out hitInfo, 100))        
        {
            GameObject go = hitInfo.collider.gameObject; 
            if (go.tag=="Player")
            {
                GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
        }
    }
}
