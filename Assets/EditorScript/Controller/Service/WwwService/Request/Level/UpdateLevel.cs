using Newtonsoft.Json;

public class UpdateLevel : WwwRequest {

    public UpdateLevel(LevelModelComponent component) {
        urlData.Add("levels");
        urlData.Add("update");
        postData.Add("data", JsonConvert.SerializeObject(component, Formatting.None, new JsonConverter[] { new Vector2Converter() }));
    }

    public override void ParseResult() { }
}
