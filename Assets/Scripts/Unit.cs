using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public MapManager mapManager;
    public GameObject goal;
    public Rigidbody rb;

    protected Vector3 spawnPosition;
    public Vector3 previousPosition;
    protected Vector3 nextPosition;

    protected float timerMove = 0;
    protected float timerFlash = 0;

    // flashing
    public MeshRenderer meshRenderer;
    public float flashInterval = 0.1f;
    public float flashTimeLength = 1.5f;
    protected bool fadeIn = true;

    protected bool enableInput = false;

    protected Coroutine co;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        OnStart();
    }

    void Init()
    {
        spawnPosition = this.transform.position;
        previousPosition = this.transform.position;
        enableInput = true;
        goal = GameObject.Find("Goal");
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
    }

    public virtual void OnStart() { }

    // Update is called once per frame
    void Update()
    {
        //rb.WakeUp();
        if (enableInput == false)
        {
            timerFlash += Time.deltaTime;
            if (timerFlash < flashTimeLength)
            {
                return;
            }
            timerFlash = 0;
            StopCoroutine(co);
            meshRenderer.enabled = true;
            enableInput = true;
        }

        OnUpdate();
    }

    public virtual void OnUpdate() { }

    IEnumerator Toggler()
    {
        while (true)
        {
            yield return new WaitForSeconds(flashInterval);
            fadeIn = !fadeIn;
            meshRenderer.enabled = fadeIn;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //rb.Sleep();
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            co = StartCoroutine(Toggler());

            this.transform.position = spawnPosition;
            enableInput = false;
            return;
        }

        if (collision.gameObject.CompareTag("AirWall"))
        {
            if (this.gameObject.tag == "Player")
            {
                co = StartCoroutine(Toggler());
                Debug.Log("collision with AirWall");

                this.transform.position = spawnPosition;
                enableInput = false;
            }else if(this.gameObject.tag == "Enemy")
            {
                Debug.Log("enemy collision with AirWall");
                this.transform.position = previousPosition;
            }
        }
    }
}
