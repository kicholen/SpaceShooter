using Newtonsoft.Json;

public class GetPath : WwwRequest {

    public PathModelComponent Component;

    public GetPath(string pathId) {
        urlData.Add("paths");
        urlData.Add(pathId);
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<PathModelComponent>(result);
    }
}
