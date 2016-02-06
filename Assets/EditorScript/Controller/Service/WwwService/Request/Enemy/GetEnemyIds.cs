using Newtonsoft.Json;
using System.Collections.Generic;

public class GetEnemyIds : WwwRequest {

    public Dictionary<long, string> EnemyIds;

    public GetEnemyIds() {
        urlData.Add("ships");
    }

    public override void ParseResult() {
        EnemyIds = JsonConvert.DeserializeObject<Dictionary<long, string>>(result);
    }
}
