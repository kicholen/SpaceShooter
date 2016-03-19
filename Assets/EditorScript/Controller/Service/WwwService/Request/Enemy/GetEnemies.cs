using Newtonsoft.Json;
using System.Collections.Generic;

public class GetEnemies : WwwRequest {

    public List<EnemyModel> Enemies;

    public GetEnemies() {
        urlData.Add("ships/all");
    }

    public override void ParseResult() {
        Enemies = JsonConvert.DeserializeObject<List<EnemyModel>>(result);
    }
}

