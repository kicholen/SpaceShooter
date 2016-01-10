using Newtonsoft.Json;

public class GetPath : WwwRequest {

    public PathModelComponent Component;

    public GetPath(long id) {
        urlData.Add("paths");
        urlData.Add(id.ToString());
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<PathModelComponent>(result);
    }
}
