using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour {

    public int dmg; //dmg that will be given to castle when collided
    public int speed;  //speed movment
    public int hp; //hp of monster

    Vector3 goal; //moving goal. set as a position of castle
    Vector3 respawn;  //respawn position set by random
    private NavMeshAgent agent;  //agent to set destination, speed etc..

    [SerializeField]
    private int cost; //give cost money to the gameCtrl gold
    private GameObject sys; //access to gamectrl

    [SerializeField]
    private GameObject castle; //access to castle

	// Use this for initialization
	void Start () {
        //castle = GameObject.FindWithTag("Castle");
        sys = GameObject.Find("Main Camera"); //to get access to GameCtrl. GameCtrl is attached to main camera object
        goal = castle.transform.position; //goal that monster should head to
        respawn = transform.position; //where the monster is spawned
        agent = GetComponent<NavMeshAgent>(); //set monster goal, speed
        agent.destination = goal;
        agent.speed = speed;
    }

    void OnTriggerEnter(Collider other) //when collided
    {
        
        if(other.gameObject.CompareTag("Projectile")) //if it's projectile, get damage from it
        {
            
            Shooting st = other.GetComponent<Shooting>();
            hp -= st.dmg;
            Debug.Log(hp);
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		if(hp <= 0) //if hp below 0, give money to gold in GameCtrll, destroy
        {
            //개수 감소를 처리하고, 골드를 준다. 그 후 게임 오브젝트를 삭제시킨다.
            Debug.Log("Monster has destroyed by low hp");
            sys.SendMessage("earnMoney", cost);
            Destroy(this.gameObject);
        }
        if(transform.position.y <=- 50) // If somehow monster is initated at wrong position and fall down, auto destroy it
        {
            Debug.Log("Monster has destroyed by position error");
            Destroy(gameObject); 
        }
        //dagent.speed = speed;
	}
}
