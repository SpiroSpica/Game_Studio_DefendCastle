using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public int dmg;
    public int speed;
    // Use this for initialization
    public GameObject target;
	void Start () {
		
	}
	
    void getTarget(GameObject tar) //set target
    {
        target = tar;
    }

	// Update is called once per frame
	void Update () { //move toward the target
        float step = speed * Time.deltaTime;
        if (target == null)
            Destroy(this.gameObject);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }
}
