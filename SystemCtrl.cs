using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SystemCtrl : MonoBehaviour {

    // Use this for initialization
    public static SystemCtrl control;

    public int progress;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else
        {
            Destroy(gameObject);
        }
        load();
    }

    void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveInfo.dat");

        SaveData sav = new SaveData();
        sav.progress = progress;

        bf.Serialize(file, sav);
        file.Close();
    
    }

    void load()
    {
        if(File.Exists(Application.persistentDataPath + "/saveInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveInfo.dat", FileMode.Open);
            SaveData sav = (SaveData)bf.Deserialize(file);
            file.Close();

            progress = sav.progress;
        }
    }

    public void ChangeScene(int order)
    {
        SceneManager.LoadScene(order);
    }
}

[System.Serializable]
public class SaveData
{
    public int progress;
}