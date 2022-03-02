//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapManager : MonoBehaviour
{
    public List<bool> wallList = new List<bool>(new bool[9]);
    public List<GameObject> blockList = new List<GameObject>();
    public Dictionary<int, Vector3> blockLocationDic = new Dictionary<int, Vector3>();
    public GameObject block;
    public GameObject airWall;
    public GameObject goal;

    public bool enablePaint = false;

    public List<Vector3> wallLocationList = new List<Vector3>();
    public List<Vector3> legalLocationList = new List<Vector3>();
    private List<bool> paintedBlockList = new List<bool>(new bool[9]);
    bool isAllPainted = false;

    GameObject go;
    private Vector3 locNew;
    int locationIndex = 0;

    void Start()
    {
        Init();
    }

    void Init()
    {
        goal = GameObject.Find("Goal");  

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                locNew = new Vector3(j, 0, i);
                go = Instantiate(block, locNew, Quaternion.identity, this.transform);
                blockList.Add(go);
                blockLocationDic.Add(locationIndex, locNew);
                if (enablePaint == false)
                {
                    go.GetComponent<PaintFloorColor>().enabled = false;
                }

                if (wallList[locationIndex] == true)
                {
                    var renderer = blockList[locationIndex].GetComponent<Renderer>();
                    renderer.material.SetColor("_Color", Color.black);
                    Instantiate(airWall, blockList[locationIndex].transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    wallLocationList.Add(locNew + new Vector3(0, 1, 0));
                }
                else
                {
                    legalLocationList.Add(locNew + new Vector3(0, 1, 0));
                    if (enablePaint)
                    {
                        var renderer = blockList[locationIndex].GetComponent<Renderer>();
                        renderer.material.SetColor("_Color", Color.cyan);
                    }
                }

                locationIndex++;
            }
        }

        if (GameObject.FindGameObjectsWithTag("Portal") != null)
        {
            GameObject[] portalArray = GameObject.FindGameObjectsWithTag("Portal");
            foreach (var item in portalArray)
            {
                legalLocationList.Add(item.gameObject.transform.position);
            }
        }
    }

    void Update()
    {
        if (isAllPainted)
        {
            return;
        }
        if (enablePaint)
        {
            CheckColorStatus();
        }
    }

    void CheckColorStatus()
    {
        if (paintedBlockList.Count(x => x == true) == wallList.Count - wallLocationList.Count)
        {
            isAllPainted = true;
            //colorDelegate?.Invoke(isAllPainted);
            SetGoalFree();

            // kill the script
            Debug.Log("All color blocks painted!");

        }
        for (int i = 0; i < wallList.Count; i++)
        {
            if (paintedBlockList[i] == true)
            {
                continue;
            }
            if (wallList[i] == false)
            {
                var renderer = blockList[i].GetComponent<Renderer>();
                if (renderer.material.color == Color.red)
                {
                    paintedBlockList[i] = true;
                }
            }
        }
    }

    void SetGoalFree()
    {
        for (int i = 0; i < wallList.Count; i++)
        {
            if (wallList[i] == true)
            {
                var renderer = blockList[i].GetComponent<Renderer>();
                if (blockList[i].transform.position + new Vector3(0, 1, 0) == goal.transform.position
                    && renderer.material.color == Color.black)
                {
                    wallList[i] = false;
                    renderer.material.SetColor("_Color", Color.white);
                    foreach (var item in GameObject.FindGameObjectsWithTag("AirWall"))
                    {
                        if (item.transform.position == goal.transform.position)
                        {
                            Destroy(item);
                            wallLocationList.Remove(goal.transform.position - new Vector3(0, 1, 0));
                        }
                    }
                }
            }
        }
    }
}
