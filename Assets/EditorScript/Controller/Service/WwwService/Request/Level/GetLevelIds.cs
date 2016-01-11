using Newtonsoft.Json;
using System.Collections.Generic;

public class GetLevelIds : WwwRequest {

    public List<string> LevelIds;

    public GetLevelIds() {
        urlData.Add("levels");
    }

    public override void ParseResult() {
        LevelIds = JsonConvert.DeserializeObject<List<string>>(result);
    }
}
