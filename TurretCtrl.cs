using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretCtrl : MonoBehaviour {

    // Use this for initialization
    public GameObject proj; //shoot GameObject
    public int dmg;  //damage that will be given to mosnter. if projectile is initated, this value is given to the projectile
    public float delay; //delay for each shooting
    public float range; //range that turret recognizes
    public float disappear; //this is the life span of turret. if it is 10, turret disappears after 10 seconds
    public int attackNum;

    private GameObject IC; //To access to projectile 
    private bool act = false; //if this is false, nothing works. only works when true

    GameObject[] InRange = new GameObject[50];


    void Start () {
        InvokeRepeating("shoot",0,delay);
	}

    void gone()
    {
        Destroy(this.gameObject);
    }

    void shoot()
    {
        if (act)
        {
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position - new Vector3(0, -5, 0), range);
            int i = 0;
            int save = -1;
            float small = 987654321;
            int seekNum = attackNum;
            while (i < hitColliders.Length && seekNum != 0)
            {
                if (hitColliders[i].CompareTag("Monster"))
                {

                    /*Vector3 diff = transform.position - hitColliders[i].transform.position;
                    float cur = diff.sqrMagnitude;
                    if (cur < small)
                    {
                        small = cur;
                        save = i;
                    }
                    Debug.Log(save);
                    */
                    IC = GameObject.Instantiate(proj, transform.position + new Vector3(0, 4, 0), new Quaternion(0, 0, 0, 0));
                    Shooting st = IC.GetComponent<Shooting>();
                    st.dmg = dmg;
                    st.target = hitColliders[i].gameObject;
                    seekNum--;
                }
                i++;
            }
            /*
            if (save != -1)
            {
                IC = GameObject.Instantiate(proj, transform.position + new Vector3(0, 4, 0), new Quaternion(0, 0, 0, 0));
                Shooting st = IC.GetComponent<Shooting>();
                st.dmg = dmg;
                st.target = hitColliders[save].gameObject;

            }*/
        }
    }
    
    void active()
    {
        act = true;
        Invoke("gone", disappear);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
