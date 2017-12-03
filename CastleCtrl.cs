using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleCtrl : MonoBehaviour {

    public int HP;
    public int breakPoint;
    // Use this for initialization
    private int initHP;
    private int breakFlag = 1;
    private GameObject sys;
    Rigidbody rigid;
    GameCtrl gc;
	void Start () {
        initHP = HP;
        rigid = GetComponent<Rigidbody>();
        sys = GameObject.Find("Main Camera");
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Monster reaches Castle");
            MonsterCtrl mc = other.gameObject.GetComponent<MonsterCtrl>();
            HP -= mc.dmg;
            mc.hp = 0;
        }
    }

    void BreakDown()
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

    // Update is called once per frame
    void Update () {
        if(HP <= initHP/2 && breakFlag == 1)
        {
            BreakDown();
            breakFlag = 0;
        }
		if(HP <= 0)
        {
            rigid.constraints &= ~RigidbodyConstraints.FreezePositionY;
            gc = sys.GetComponent<GameCtrl>();
            gc.gameEnd = true;
        }
	}
}
