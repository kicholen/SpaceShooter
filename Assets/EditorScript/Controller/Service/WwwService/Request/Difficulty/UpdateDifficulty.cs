using Newtonsoft.Json;

public class UpdateDifficulty : WwwRequest {
    public UpdateDifficulty(DifficultyModelComponent component) {
        urlData.Add("difficulties");
        urlData.Add("update");
        postData.Add("data", JsonConvert.SerializeObject(component, Formatting.None, new JsonConverter[] { new Vector2Converter() }));
    }

    public override void ParseResult() { }
}
