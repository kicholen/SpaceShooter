using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

public class BonusFaker : BaseFaker
{
    List<BonusModelComponent> components;

    public BonusFaker() : base()
    {
        components = LoadAll<BonusModelComponent>();
    }

    public void Create(CreateBonus request)
    {
        request.Component = new BonusModelComponent();
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

    public void Get(GetBonus request)
    {
        int id = Convert.ToInt32(request.urlData[1]);
        request.Component = components.First(cmp => cmp.id.Equals(id));
    }

    public void GetIds(GetBonusIds request)
    {
        request.BonusIds = new Dictionary<long, string>();

        for (int i = 0; i < components.Count; i++)
            request.BonusIds.Add(components[i].id, components[i].type.ToString());
    }

    public void GetAll(GetBonuses request)
    {
        request.Bonuses = new List<BonusModelComponent>(components);
    }

    public void Update(UpdateBonus request)
    {
        BonusModelComponent cmp = JsonConvert.DeserializeObject<BonusModelComponent>(request.postData["data"]);
        Serialize(cmp, cmp.id);
    }
}