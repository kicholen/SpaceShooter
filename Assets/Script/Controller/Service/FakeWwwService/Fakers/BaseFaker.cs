using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BaseFaker
{
    protected HashSet<int> ids;

    public BaseFaker()
    {
        ids = new HashSet<int>();
    }

    protected void Delete<T>(int id)
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        //File.Delete(Application.dataPath + "/Resources/" + typeof(T).Name + "_" + id + ".json");
#endif
    }

    protected List<T> LoadAll<T>()
    {
        List<T> components = new List<T>();
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        FileInfo[] infos = new DirectoryInfo(Application.dataPath + "/Resources/").GetFiles();
        foreach (FileInfo info in infos)
        {
            if (info.Name.StartsWith((typeof(T)).Name) && info.Name.EndsWith(".json"))
            {
                int id = Convert.ToInt32(info.Name.Split('_')[1].Split('.')[0]);
                ids.Add(id);
                components.Add(Utils.Deserialize<T>(id.ToString()));
            }
        }
#endif
        return components;
    }

    protected int GetUniqueRandomId()
    {
        int id = 0;
        do
            id = UnityEngine.Random.Range(5000, short.MaxValue);
        while (ids.Contains(id));
        return id;
    }

    protected void Serialize<T>(T value, long id)
    {
        string path = Application.dataPath + "/Resources/" + value.GetType().Name + "_" + id + ".json";
        JsonSerializer serializer = new JsonSerializer();
        StreamWriter streamWriter = new StreamWriter(path, false);
        serializer.Serialize(streamWriter, value);
        streamWriter.Close();
    }
}