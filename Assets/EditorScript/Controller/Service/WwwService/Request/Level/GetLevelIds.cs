using Newtonsoft.Json;
using System.Collections.Generic;

public class GetLevelIds : WwwRequest {

    public Dictionary<long, string> LevelIds;

    public GetLevelIds() {
        urlData.Add("levels");
    }

    public override void ParseResult() {
        LevelIds = JsonConvert.DeserializeObject<Dictionary<long, string>>(result);
    }
}
