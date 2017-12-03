using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrl : MonoBehaviour {

    public int num;
    public int resTime;
    public int amount;
    public GameObject Mons;
    //public GameObject Effect;
    //public GameObject Turret;
    //public Button bt1; //현재 클릭 인식 자체가 안되는 문제가 있다.

    private Vector3 pos;
    private Quaternion rot;
    //private bool SummonFlag;
    private bool gameEnd = false;

    GameObject IC;
    Vector3 newPosition;

    RaycastHit hit;
    int layerNum = 1 << 8;
    void Start () {
        InvokeRepeating("FinishCheck", 5, 5);
        InvokeRepeating("SummonMons", 1, resTime);
    //    bt1.onClick.AddListener(TaskOnClick);
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
            Debug.Log("Game has ended");
            CancelInvoke("SummonMons");
        }
    }

    void TaskOnClick()
    {
        Debug.Log("Clicked");
        /*
        //if (game.money < cost)
        //    Debug.Log("You don't have enought Money");
        //else
        //{
            IC = GameObject.Instantiate(Turret, newPosition, new Quaternion(0, 0, 0, 0)) as GameObject;
            SummonFlag = true;
            //game.SendMessage("MoneyUse", cost);
        
        */
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
    
    

	// Update is called once per frame
	void Update () {
        CameraMove();
        //TurretSum();
    }
}
