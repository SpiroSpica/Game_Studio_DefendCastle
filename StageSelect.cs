using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private Button[] bt;
    [SerializeField]
    private Button bt2;

    void Start () {
        Debug.Log(bt.Length);
        for(int i=0; i<bt.Length;i++)
        {
            int a = i;
            bt[a].onClick.AddListener(delegate { TaskOnClick(a); });
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
