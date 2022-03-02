using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public delegate void MoveDelegate(Vector3 direction);
    public MoveDelegate moveDelegate;

    public delegate void SuccessDelegate(bool isRestart);
    public SuccessDelegate successDelegate;

    float speed = 10f;
    public GameObject bulletTemplate;
    GameObject bulletCopy;

    public override void OnUpdate()
    {
        Move();
        Success();
        Fire();
    }

    public void Move()
    {
        previousPosition = this.transform.position;

        MoveLegalLoc();
    }

    void MoveLegalLoc()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            nextPosition = this.transform.position + Vector3.forward;
            if (mapManager.legalLocationList.Contains(nextPosition) || mapManager.wallLocationList.Contains(nextPosition))
            {
                transform.Translate(Vector3.forward);
                moveDelegate?.Invoke(Vector3.forward);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            nextPosition = this.transform.position + Vector3.back;
            if (mapManager.legalLocationList.Contains(nextPosition) || mapManager.wallLocationList.Contains(nextPosition))
            {
                transform.Translate(Vector3.back);
                moveDelegate?.Invoke(Vector3.back);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            nextPosition = this.transform.position + Vector3.left;
            if (mapManager.legalLocationList.Contains(nextPosition) || mapManager.wallLocationList.Contains(nextPosition))
            {
                transform.Translate(Vector3.left);
                moveDelegate?.Invoke(Vector3.left);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            nextPosition = this.transform.position + Vector3.right;
            if (mapManager.legalLocationList.Contains(nextPosition) || mapManager.wallLocationList.Contains(nextPosition))
            {
                transform.Translate(Vector3.right);
                moveDelegate?.Invoke(Vector3.right);
            }
        }
    }

    public void Success()
    {
        Vector2 a = new Vector2(transform.position.x, transform.position.z);
        Vector2 b = new Vector2(goal.transform.position.x, goal.transform.position.z);

        if (Vector2.Distance(a, b) < 0.1f)
        {
            StartCoroutine(WaitForCheckSuccess());
        }
    }

    IEnumerator WaitForCheckSuccess()
    {
        yield return new WaitForSeconds(0.25f);

        Vector2 a = new Vector2(transform.position.x, transform.position.z);
        Vector2 b = new Vector2(goal.transform.position.x, goal.transform.position.z);

        if (Vector2.Distance(a, b) < 0.1f)
        {
            Debug.Log("Success!");
            successDelegate?.Invoke(false);
            enableInput = false;
        }
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bulletCopy = Instantiate(bulletTemplate, transform.position, transform.rotation);
            bulletCopy.GetComponent<Element>().direction = Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            bulletCopy = Instantiate(bulletTemplate, transform.position, transform.rotation);
            bulletCopy.GetComponent<Element>().direction = Vector3.back;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            bulletCopy = Instantiate(bulletTemplate, transform.position, transform.rotation);
            bulletCopy.GetComponent<Element>().direction = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            bulletCopy = Instantiate(bulletTemplate, transform.position, transform.rotation);
            bulletCopy.GetComponent<Element>().direction = Vector3.right;
        }
    }
}
