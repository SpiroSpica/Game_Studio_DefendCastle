using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretCtrl : MonoBehaviour {

    // Use this for initialization
    public GameObject proj;
    public int dmg;
    public float delay;
    public float range;
    public float disappear;

    private GameObject IC;
    private bool act = false;

    GameObject[] InRange = new GameObject[50];


    void Start () {
        Debug.Log("Hi");
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
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].CompareTag("Monster"))
                {

                    Vector3 diff = transform.position - hitColliders[i].transform.position;
                    float cur = diff.sqrMagnitude;
                    if (cur < small)
                    {
                        small = cur;
                        save = i;
                    }
                    Debug.Log(save);
                }
                i++;
            }
            if (save != -1)
            {
                IC = GameObject.Instantiate(proj, transform.position + new Vector3(0, 4, 0), new Quaternion(0, 0, 0, 0));
                Shooting st = IC.GetComponent<Shooting>();
                st.dmg = dmg;
                st.target = hitColliders[save].gameObject;

            }
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
