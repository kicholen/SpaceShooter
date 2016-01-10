using Newtonsoft.Json;

public class CreatePath : WwwRequest {

    public PathModelComponent Component;

    public CreatePath() {
        urlData.Add("paths");
        urlData.Add("new");
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<PathModelComponent>(result);
    }
}
