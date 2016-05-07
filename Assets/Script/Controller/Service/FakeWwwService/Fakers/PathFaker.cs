using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PathFaker : BaseFaker
{
    List<PathModel> components = new List<PathModel>();
    Dictionary<long, int> idToSaveId = new Dictionary<long, int>();

    public PathFaker() : base()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        FileInfo[] infos = new DirectoryInfo(Application.dataPath + "/Resources/").GetFiles();
        foreach (FileInfo info in infos)
        {
            if (info.Name.StartsWith((typeof(PathModel)).Name) && info.Name.EndsWith(".json"))
            {
                int id = Convert.ToInt32(info.Name.Split('_')[1].Split('.')[0]);
                PathModel model = Utils.Deserialize<PathModel>(id.ToString());
                components.Add(model);
                idToSaveId.Add(model.id, id);
            }
        }
#endif
    }

    public void Create(CreatePath request)
    {
        request.Component = new PathModel();
        request.Component.id = GetUniqueRandomId();
        string name = components.OrderByDescending(cmp => Convert.ToInt16(cmp.name)).First().name;
        request.Component.name = Convert.ToInt16(name) + 1 + "";
        request.Component.points = new List<UnityEngine.Vector2>();
        components.Add(request.Component);
        idToSaveId.Add(request.Component.id, components.Count);
    }

    public void Delete<T>(T request) where T : WwwRequest
    {
        int id = Convert.ToInt32(request.urlData[2]);
        Delete<T>(id);
        components.RemoveAll(cmp => cmp.id.Equals(id));
    }

    public void Get(GetPath request)
    {
        long id = Convert.ToInt32(request.urlData[1]);
        request.Component = components.First(cmp => cmp.id.Equals(id));
    }

    public void GetIds(GetPathIds request)
    {
        request.PathIds = new Dictionary<long, string>();

        for (int i = 0; i < components.Count; i++)
            request.PathIds.Add(components[i].id, components[i].name);
    }

    public void GetAll(GetPaths request)
    {
        request.Paths = new List<PathModel>(components);
    }

    public void Update(UpdatePath request)
    {
        PathModel cmp = JsonConvert.DeserializeObject<PathModel>(request.postData["data"]);
        Serialize(cmp, idToSaveId[cmp.id]);
    }
}