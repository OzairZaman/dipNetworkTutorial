using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

using System.Xml;
using System.Xml.Serialization;

public class GameManager : MonoBehaviour
{

    public Transform player;
    public string fileName = "GameData.xml";
    private GameData data = new GameData();
    // 2 ways of getting the math
    // - mobile > Application.persistentDataPath
     

    public void Save(string _path)
    {
        var serializer = new XmlSerializer(typeof(GameData));
        // 
        var stream = new FileStream(_path, FileMode.Create);
        //
        serializer.Serialize(stream, data);
        //
        stream.Close();
        Debug.Log("File saved to " + _path);
    }

    public void Load(string _path)
    {
        var serializer = new XmlSerializer(typeof(GameData));
        // 
        var stream = new FileStream(_path, FileMode.Open);
        //
        data = serializer.Deserialize(stream) as GameData;
        //
        stream.Close();
        //Debug.Log("File saved to " + _path);
    }


    //public void NextScene()
    //{

    //}


    //// Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/" + fileName;
        Load(path);


       
    }

    //// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            player = FindObjectOfType<PlayerScript>().transform;
            data.postion = player.position;
            data.roattion = player.rotation;
            data.dialogue = new string[]
            {
                "hello",
                "world"
            };
            Save(Application.dataPath + "/" + fileName);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            Load(Application.dataPath + "/" + fileName);
            player = FindObjectOfType<PlayerScript>().transform;
            player.position = data.postion;
            player.rotation = data.roattion;

        }

    }
}
