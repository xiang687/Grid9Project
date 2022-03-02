using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public float lifeTime = 3f;
    public float speed = 10f;
    public Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * Time.deltaTime * direction;
    }
}
