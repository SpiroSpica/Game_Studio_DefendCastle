using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpeningCtrl : MonoBehaviour {

    [SerializeField]
    private GameObject HowTo;
    [SerializeField]
    private GameObject back;


    public void toStageSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void howToPlay()
    {
        HowTo.SetActive(true);
        back.SetActive(true);
    }

    public void closeHowTo()
    {
        HowTo.SetActive(false);
        back.SetActive(false);
    }
}
