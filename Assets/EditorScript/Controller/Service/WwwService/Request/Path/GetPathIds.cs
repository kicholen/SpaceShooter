using Newtonsoft.Json;
using System.Collections.Generic;

public class GetPathIds : WwwRequest {

    public Dictionary<long, string> PathIds;

    public GetPathIds() {
        urlData.Add("paths");
    }

    public override void ParseResult() {
        PathIds = JsonConvert.DeserializeObject<Dictionary<long, string>>(result);
    }
}
