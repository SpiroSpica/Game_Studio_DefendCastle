using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private Button[] bt;


    void Start () {
        int check = SystemCtrl.control.progress;

        for (int i=0; i<=Mathf.Min(bt.Length,check);i++)
        {
            int a = i;
            bt[a].onClick.AddListener(delegate { TaskOnClick(a); });
        }

        for(int i=0; i<check; i++)
        {
            bt[i].image.color = Color.blue;
        }
        for(int i = check + 1; i < bt.Length; i++)
        {
            bt[i].image.color = Color.red;
        }
    }

    void TaskOnClick(int a)
    {
        Debug.Log(a);
        SceneManager.LoadScene(a+2);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
