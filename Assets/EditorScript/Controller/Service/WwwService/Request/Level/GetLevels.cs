using Newtonsoft.Json;
using System.Collections.Generic;

public class GetLevels : WwwRequest {

    public List<LevelModelComponent> Levels;

    public GetLevels() {
        urlData.Add("levels/all");
    }

    public override void ParseResult() {
        Levels = JsonConvert.DeserializeObject<List<LevelModelComponent>>(result);
    }
}

