using Newtonsoft.Json;
using System.Collections.Generic;

public class GetPaths : WwwRequest {

    public List<PathModel> Paths;

    public GetPaths() {
        urlData.Add("paths/all");
    }

    public override void ParseResult() {
        Paths = JsonConvert.DeserializeObject<List<PathModel>>(result);
    }
}
