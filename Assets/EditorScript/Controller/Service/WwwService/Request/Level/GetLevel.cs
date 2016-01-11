using Newtonsoft.Json;

public class GetLevel : WwwRequest {

    public LevelModelComponent Component;

    public GetLevel(long id) {
        urlData.Add("levels");
        urlData.Add(id.ToString());
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<LevelModelComponent>(result);
    }
}
