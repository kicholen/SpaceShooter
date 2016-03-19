using Newtonsoft.Json;

public class GetPath : WwwRequest {

    public PathModel Component;

    public GetPath(long id) {
        urlData.Add("paths");
        urlData.Add(id.ToString());
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<PathModel>(result);
    }
}
