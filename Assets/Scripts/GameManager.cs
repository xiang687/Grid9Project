using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject levelLoader;
    public GameObject menuManagerGameObject;
    public GameObject audioManagerGameObject;

    void Awake()
    {
        Instantiate(audioManagerGameObject);
        Instantiate(menuManagerGameObject);
        //Initialize(true);
        //menuManager.GetComponent<MenuManager>().playDelegateEvent += Initialize;
        //menuManager.GetComponent<MenuManager>().playDelegate = Initialize;
    }

    //public void Initialize(bool isRestart)
    //{
    //    if (isRestart)
    //    {
    //        //Instantiate(levelLoader);
    //    }
    //}

}
