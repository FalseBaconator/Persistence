using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public float hp;
    public float xp;

    // Start is called before the first frame update
    void Awake()
    {
        if(singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Saves Data to File (doesn't work on web)
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);

        SaveData data = new SaveData();
        data.hp = hp;
        data.xp = xp;

        bf.Serialize(file, data);
        file.Close();
    }

    //Loads data from File (doesn't work on web)
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            hp = data.hp;
            xp = data.xp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
class SaveData
{
    public float hp;
    public float xp;
}
