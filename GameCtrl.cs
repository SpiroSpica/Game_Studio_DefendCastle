﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameCtrl : MonoBehaviour {

    [SerializeField]
    public int num;
    [SerializeField]
    public int resTime;
    [SerializeField]
    public int amount;
    [SerializeField]
    public GameObject Mons;
    [SerializeField]
    public GameObject Turret;
    public int cost;
    [SerializeField]
    public Button bt1;
    [SerializeField]
    public GameObject Castle;
    public bool gameEnd = false;

    
    [SerializeField]
    private GameObject Effect;
    [SerializeField]
    private int gold = 0;

    private Vector3 pos;
    private Quaternion rot;
    private bool SummonFlag;
    private NavMeshAgent tmpNav;
    GameObject IC;
    Vector3 newPosition;
    RaycastHit hit;
    private GameObject txt;
    private Text info;
    int layerNum = 1 << 8;

    void Start () {
        InvokeRepeating("FinishCheck", 1, 1);
        InvokeRepeating("GainMoney", 1, 3);
        InvokeRepeating("SummonMons", 1, resTime);
        bt1.onClick.AddListener(TaskOnClick);
        txt = GameObject.Find("Canvas/Information");
        info = txt.GetComponent<Text>();
    }
	
    private void GainMoney()
    {
        gold += amount;
    }
    private void SummonMons()
    {
        num--;
        if (num >= 0)
        {
            int x, z;
            while (true)
            {
                x = Random.Range(0, 200);
                z = Random.Range(0, 200);
                if (x <= 50 || z <= 50)
                    break;
            }
            pos = new Vector3(x, 133, z); //133은 terrain의 위치이다.
            rot = new Quaternion(0, 0, 0, 0);
            Instantiate(Mons, pos, rot);
        }
    }
    private void FinishCheck()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Monster");
        if (gos.Length == 0)
            gameEnd = true;
        if (gameEnd == true)
        {
            for(int i=0; i<gos.Length; i++)
            {
                Debug.Log("Stop Them!");
                tmpNav = gos[i].GetComponent<NavMeshAgent>();
                tmpNav.speed = 0;
            }
            Debug.Log("Game has ended");
            CancelInvoke("SummonMons");
        }
    }
    void TaskOnClick()
    {
        Debug.Log("Clicked");

        if (gold < cost)
            Debug.Log("You don't have enough Money");
        else
        {
            IC = GameObject.Instantiate(Turret, newPosition, new Quaternion(0, 0, 0, 0)) as GameObject;
            Debug.Log("Summon");
            SummonFlag = true;
            gold -= cost;
        }
        
    }
    void CameraMove()
    {
        if (Input.GetKey("w"))
        {
            if (this.transform.position.z <= 200 && this.transform.position.x <= 200)
                this.transform.position += new Vector3(1, 0, 1);
        }
        if (Input.GetKey("s"))
        {
            if (this.transform.position.z >= -20 && this.transform.position.x >= -20)
                this.transform.position += new Vector3(-1, 0, -1);
        }
        if (Input.GetKey("a"))
        {
            if (this.transform.position.z <= 200 && this.transform.position.x >= -20)
                this.transform.position += new Vector3(-1, 0, 1);
        }
        if (Input.GetKey("d"))
        {
            if (this.transform.position.z >= -20 && this.transform.position.x <= 200)
                this.transform.position += new Vector3(1, 0, -1);
        }
        if (Input.GetKey("up"))
        {
            if (this.transform.position.y <= 250)
                this.transform.position += new Vector3(0, 1, 0);
        }
        if (Input.GetKey("down"))
        {
            if (this.transform.position.y >= 150)
                this.transform.position += new Vector3(0, -1, 0);
        }
    }
    void TurretSum()
    {
        if (SummonFlag == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            {

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerNum) && hit.point.x >= 0 && hit.point.z >= 0)
                {
                    newPosition = hit.point;
                    newPosition.y += 3;
                    if (hit.point.y <= 500)
                    {
                        IC.transform.position = newPosition;
                    }
                }

            }
        }
        if (SummonFlag == true && Input.GetMouseButton(0))
        {
            Instantiate(Effect, newPosition - new Vector3(0, 2.0f, 0), new Quaternion(0, 0, 0, 0));
            SummonFlag = false;
            IC.SendMessage("active");
        }
    }
    void earnMoney(int a)
    {
        gold += a;
    }
	// Update is called once per frame
	void Update () {
        CameraMove();
        TurretSum();
        info.text = "Gold : " + gold + "\n" + "Number of Enemies Left : " + num;
    }
}
