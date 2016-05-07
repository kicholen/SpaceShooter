using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class LevelFaker : BaseFaker
{
    List<LevelModelComponent> components;

    public LevelFaker() : base()
    {
        components = LoadAll<LevelModelComponent>();
    }

    public void Create(CreateLevel request)
    {
        request.Component = new LevelModelComponent();
        request.Component.id = GetUniqueRandomId();
        request.Component.name = "new_level";
        request.Component.waves = new List<WaveSpawnModel>();
        request.Component.enemies = new List<EnemySpawnModel>();
        components.Add(request.Component);
    }

    public void Delete<T>(T request) where T : WwwRequest
    {
        int id = Convert.ToInt32(request.urlData[2]);
        Delete<T>(id);
        components.RemoveAll(cmp => cmp.id.Equals(id));
    }

    public void Get(GetLevel request)
    {
        long id = Convert.ToInt32(request.urlData[1]);
        request.Component = Utils.Deserialize<LevelModelComponent>(id.ToString());
    }

    public void GetIds(GetLevelIds request)
    {
        request.LevelIds = new Dictionary<long, string>();

        for (int i = 0; i < components.Count; i++)
            request.LevelIds.Add(components[i].id, components[i].name);
    }

    public void GetAll(GetLevels request)
    {
        request.Levels = new List<LevelModelComponent>(components);
    }

    public void Update(UpdateLevel request)
    {
        LevelModelComponent cmp = JsonConvert.DeserializeObject<LevelModelComponent>(request.postData["data"]);
        Serialize(cmp, cmp.id);
    }
}