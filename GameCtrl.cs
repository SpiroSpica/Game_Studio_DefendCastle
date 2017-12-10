using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

//You better change the instantiating monster in to array!!!!
[System.Serializable]
public class Mon
{
    public int num;
    public int resTime;
    public GameObject Mons;
};


public class GameCtrl : MonoBehaviour {

    public static GameCtrl control;


    [SerializeField]
    private Mon[] data;

    [SerializeField]
    private int amount; // amount of money to increase per each period of time

    private int timeMoney = 3; //period of time to gain money. If it is 3, player earns certain amount of money every 3 seconds

    [SerializeField]
    private GameObject Turret; //GameObejct to build turret
    public int cost; //cost that is needed to build turret 
    [SerializeField]
    private Button[] bt; //bt1 is to summon turret 1
    [SerializeField]
    private GameObject Castle; //to get the value of castle. 
    public bool gameEnd = false;  //check if system needs to continue the game or finish the game (It does not check win or lose yet)
    
    [SerializeField]
    private GameObject Effect; // Effect instantiated by building turret 1  (May need to include exit effect also
    [SerializeField]
    private int gold = 0; //current gold amount. increase when time passes and get monster, loses when building turret or (using skills which has not made yet)

    [SerializeField]
    private int height;

    [SerializeField]
    private GameObject win;
    [SerializeField]
    private GameObject fail;
    
    
    //below are about summoning turret
    private Vector3 pos; //position of turret 
    private Quaternion rot; //rotation of turret
    private bool SummonFlag; //when this is enabled, turret follows the mouse movment. If not, turret fixes its position to where mouse is.
    private NavMeshAgent tmpNav; //Make speed to 0 when game is finished -> make all monsters stop moving
    

    
    GameObject IC;  //It is to access the value of instanatiated GameObject
    Vector3 newPosition; //It is the position where mouse pointer is. Followed by using raycast
    RaycastHit hit;  //hit point to get the newPosition above

    //These are for the UI information
    private GameObject txt; //to access the UI text
    private Text info; // change the value saved in the text
    [SerializeField]
    private int numTot; //show he total monsters left.
    private int monslen;
    int layerNum = 1 << 8; //This is for turret build. Make turret to only think about terrain layer
    /*
    void Awake()
    {
        if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    */

    void Start () { //Initiate

        monslen = data.Length;
        StartCoroutine(FinishCheck());
        numTot = 0;
        for (int i=0; i<monslen; i++)
        {
            StartCoroutine(SummonMons(data[i].resTime, data[i].Mons, data[i].num));
            numTot += data[i].num;
        }
        StartCoroutine(GainMoney(amount, timeMoney)); //Gain ceratin amount of money at period of time
        for(int i=0; i<bt.Length;i++)
        {
            bt[i].onClick.AddListener(delegate { TaskOnClick(i); });
        }
        txt = GameObject.Find("Canvas/Information");  //access to UI text information
        info = txt.GetComponent<Text>(); //aaccess to text itself
    }
	
    private IEnumerator GainMoney(int amount, int timeMoney)  //increase amount at gold every timeMoney seconds
    {
        while (!gameEnd)  //stop giving money when game ends
        {
            gold += amount;
            yield return new WaitForSeconds(timeMoney);
        }
    }
    private IEnumerator SummonMons(int resTime, GameObject Mons, int numMons) //summon numMons of Mons by summoning a Mons every resTime
    {
        while(!gameEnd && numMons>0)
        {
            int x, z;
            while (true)
            {
                x = Random.Range(0, 200);
                z = Random.Range(0, 200);
                if (x <= 50 || z <= 50)
                    break;
            }
            pos = new Vector3(x, height, z); //133은 terrain의 위치이다.
            rot = new Quaternion(0, 0, 0, 0);
            Instantiate(Mons, pos, rot);
            yield return new WaitForSeconds(resTime);
            numMons--;
            numTot--;
        }
    }
    private IEnumerator FinishCheck()  //Find Monster Objects with tag. If its number is 0, make gameEnd to true. As this function only sees gameEnd variable, if this value is changed by other function, it still works as gameEnd
    {
        bool winFlag = false;
        while (!gameEnd)
        {
            yield return new WaitForSeconds(3);
            GameObject[] gos;
            
            gos = GameObject.FindGameObjectsWithTag("Monster");
            if (gos.Length == 0)
            {
                gameEnd = true;
                winFlag = true;
            }
            if (gameEnd == true)
            {
                for (int i = 0; i < gos.Length; i++)  //If monsters are left, make them stop moving
                {
                    Debug.Log("Stop Them!");
                    tmpNav = gos[i].GetComponent<NavMeshAgent>();
                    tmpNav.speed = 0;
                }
                Debug.Log("Game has ended");
                CancelInvoke("SummonMons");
            }
        }
        if(gameEnd)
        {
            
            if (winFlag)
                win.SetActive(true);
            else
                fail.SetActive(true);
            Invoke("moveback",3);
        }
    }

    void moveback()
    {
        SceneManager.LoadScene(1);
    }

    void TaskOnClick(int a) //works when Clciked
    {
        Debug.Log("Clicked");
       
        //If there's enough gold, summon turret
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
    void CameraMove()  //Camera movment left, right, front, back, up, down
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
    void TurretSum()  //Turret summon -> if not clicked, move turret around. If clicked, set turret position
    {
        if (SummonFlag == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //shoot ray to the screen
            {

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerNum) && hit.point.x >= 0 && hit.point.z >= 0) //this lay only stops when collided by the object with terrain layer
                {
                    newPosition = hit.point;
                    newPosition.y += 3; //set the position. To avoid turrets from falling down.
                    if (hit.point.y <= 500)
                    {
                        IC.transform.position = newPosition;
                    }
                }

            }
        }
        if (SummonFlag == true && Input.GetMouseButton(0)) //if clicked
        {
            Instantiate(Effect, newPosition - new Vector3(0, 2.0f, 0), new Quaternion(0, 0, 0, 0));
            SummonFlag = false;
            IC.SendMessage("active");
        }
    }
    void earnMoney(int a)  // this is active when another way of gold earning happens. currently it is only used when monster dies
    {
        gold += a;
    }
	// Update is called once per frame
	void Update () {
        CameraMove();
        TurretSum();
        info.text = "Gold : " + gold + "\n" + "Number of Enemies Left : " + numTot;
        if(Input.GetKey("1") && SummonFlag == false)
        {
            TaskOnClick(1);
        }
    }
}
