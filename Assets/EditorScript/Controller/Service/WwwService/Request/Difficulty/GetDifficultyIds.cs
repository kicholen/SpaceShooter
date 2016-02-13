using Newtonsoft.Json;
using System.Collections.Generic;

public class GetDifficultyIds : WwwRequest {
    public Dictionary<long, string> DifficultyIds;

    public GetDifficultyIds() {
        urlData.Add("difficulties");
    }

    public override void ParseResult() {
        DifficultyIds = JsonConvert.DeserializeObject<Dictionary<long, string>>(result);
    }
}
