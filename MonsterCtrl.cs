using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour {

    public int dmg;
    public int speed;
    public int hp;

    Vector3 goal;
    Vector3 respawn;
    private NavMeshAgent agent;

    [SerializeField]
    private int cost;
    private GameObject sys; //access to gamectrl

    [SerializeField]
    private GameObject castle;

	// Use this for initialization
	void Start () {
        //castle = GameObject.FindWithTag("Castle");
        sys = GameObject.Find("Main Camera");
        goal = castle.transform.position;
        respawn = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal;
        agent.speed = speed;
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Projectile"))
        {
            
            Shooting st = other.GetComponent<Shooting>();
            hp -= st.dmg;
            Debug.Log(hp);
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		if(hp <= 0)
        {
            //개수 감소를 처리하고, 골드를 준다. 그 후 게임 오브젝트를 삭제시킨다.
            Debug.Log("Monster has destroyed by low hp");
            sys.SendMessage("earnMoney", cost);
            Destroy(this.gameObject);
        }
        if(transform.position.y <=- 50) // 추락 버그 방지
        {
            Debug.Log("Monster has destroyed by position error");
            Destroy(gameObject); 
        }
        //dagent.speed = speed;
	}
}
