using Newtonsoft.Json;
using System.Collections.Generic;

public class GetPathIds : WwwRequest {

    public List<string> PathIds;

    public GetPathIds() {
        urlData.Add("paths");
    }

    public override void ParseResult() {
        PathIds = JsonConvert.DeserializeObject<List<string>>(result);
    }
}
