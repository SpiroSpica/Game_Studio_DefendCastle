using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleCtrl : MonoBehaviour {

    public int HP; //hp of the castle. breakdown at it's 50%, die at it's 0%
    
    // Use this for initialization
    private int initHP;
    private int breakFlag = 1;
    private GameObject sys;
    Rigidbody rigid; //for the gameEnd situation -> make turret to falldown
    GameCtrl gc; //get script information GameCtrl
	void Start () {
        initHP = HP;
        rigid = GetComponent<Rigidbody>();
        sys = GameObject.Find("Main Camera");
	}

    void OnTriggerEnter(Collider other) //if collided by Monster, get damage that monster has, make monster's hp to 0 to destroy it
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Monster reaches Castle");
            MonsterCtrl mc = other.gameObject.GetComponent<MonsterCtrl>();
            HP -= mc.dmg;
            mc.hp = 0;
        }
    }

    void BreakDown()  //add rigidbody to inner parts, lead parts to have collisions and explode.
    {
        int childNum = this.transform.childCount;
        for (int i = 0; i < childNum; i++)
        {
            if (transform.GetChild(i).gameObject.CompareTag("Breaks"))
            {
                transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
            }
        }
    }

    void Des()  //destroy
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
        if(HP <= initHP/2 && breakFlag == 1) //breakdown condition
        {
            BreakDown();
            breakFlag = 0;
        }
		if(HP <= 0) //dead condition
        {
            rigid.constraints &= ~RigidbodyConstraints.FreezePositionY;
            gc = sys.GetComponent<GameCtrl>();
            gc.gameEnd = true;
            Invoke("Des", 3);
        }
	}
}
