using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

public class DifficultyFaker : BaseFaker
{
    List<DifficultyModelComponent> components;

    public DifficultyFaker() : base()
    {
        components = LoadAll<DifficultyModelComponent>();
    }

    public void Create(CreateDifficulty request)
    {
        request.Component = new DifficultyModelComponent();
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

    public void Get(GetDifficulty request)
    {
        int id = Convert.ToInt32(request.urlData[1]);
        request.Component = components.First(cmp => cmp.id.Equals(id));
    }

    public void GetIds(GetDifficultyIds request)
    {
        request.DifficultyIds = new Dictionary<long, string>();

        for (int i = 0; i < components.Count; i++)
            request.DifficultyIds.Add(components[i].id, components[i].type.ToString());
    }

    public void GetAll(GetDifficulties request)
    {
        request.Difficulties = new List<DifficultyModelComponent>(components);
    }

    public void Update(UpdateDifficulty request)
    {
        DifficultyModelComponent cmp = JsonConvert.DeserializeObject<DifficultyModelComponent>(request.postData["data"]);
        Serialize(cmp, cmp.id);
    }
}