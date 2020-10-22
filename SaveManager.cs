using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public SaveData data;

    public bool hasLoaded;

    private void Awake()
    {
        instance = this;

        Load();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            Save();
        if (Input.GetKeyDown(KeyCode.L))
            Load();
        if (Input.GetKeyDown(KeyCode.M))
            DeleteSave();
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        BinaryFormatter bf = new BinaryFormatter();
        var file = new FileStream(dataPath + "/" + data.saveName + ".save", FileMode.Create);
        bf.Serialize(file, data);

        file.Close();
        Debug.Log("File Saved!");
    }

    public void Load()
    {
        
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + data.saveName + ".save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            var file = new FileStream(dataPath + "/" + data.saveName + ".save", FileMode.Open);

            data = bf.Deserialize(file) as SaveData;
            file.Close();
            Debug.Log("File Loaded!");
        }
        else
            Debug.Log("No save data!");

        hasLoaded = true;
    }

    public void DeleteSave()
    {

        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + data.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + data.saveName + ".save");
            Debug.Log("Deleted save data!");
        }

    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;

    public Point respawnPos;
    
    public bool doorOpen;

    public int lives;

}

[System.Serializable]
public class Point
{
    public float x;
    public float y;
    public float z;

    public Point(Vector3 p)
    {
        x = p.x;
        y = p.y;
        z = p.z;
    }

    public Vector3 toVector()
    {
        return new Vector3(x, y, z);
    }
}