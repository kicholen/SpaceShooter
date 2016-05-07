using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

public class EnemyFaker : BaseFaker
{
    List<EnemyModel> components;

    public EnemyFaker() : base()
    {
        components = LoadAll<EnemyModel>();
    }

    public void Create(CreateEnemy request)
    {
        request.Component = new EnemyModel();
        request.Component.id = GetUniqueRandomId();
        request.Component.type = request.Component.id;
        components.Add(request.Component);
    }

    public void Delete<T>(T request) where T : WwwRequest
    {
        int id = Convert.ToInt32(request.urlData[2]);
        Delete<T>(id);
        components.RemoveAll(cmp => cmp.id.Equals(id));
    }

    public void Get(GetEnemy request)
    {
        int id = Convert.ToInt32(request.urlData[1]);
        request.Component = components.First(cmp => cmp.id.Equals(id));
    }

    public void GetIds(GetEnemyIds request)
    {
        request.EnemyIds = new Dictionary<long, string>();

        for (int i = 0; i < components.Count; i++)
            request.EnemyIds.Add(components[i].id, components[i].type.ToString());
    }

    public void GetAll(GetEnemies request)
    {
        request.Enemies = new List<EnemyModel>(components);
    }

    public void Update(UpdateEnemy request)
    {
        EnemyModel cmp = JsonConvert.DeserializeObject<EnemyModel>(request.postData["data"]);
        Serialize(cmp, cmp.id);
    }
}