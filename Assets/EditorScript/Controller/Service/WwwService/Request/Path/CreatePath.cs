using Newtonsoft.Json;

public class CreatePath : WwwRequest {

    public PathModel Component;

    public CreatePath() {
        urlData.Add("paths");
        urlData.Add("new");
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<PathModel>(result);
    }
}
