using Newtonsoft.Json;

public class CreateLevel : WwwRequest {

    public LevelModelComponent Component;

    public CreateLevel() {
        urlData.Add("levels");
        urlData.Add("new");
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<LevelModelComponent>(result);
    }
}
