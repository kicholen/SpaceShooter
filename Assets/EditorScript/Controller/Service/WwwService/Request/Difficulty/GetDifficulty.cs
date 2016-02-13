using Newtonsoft.Json;

public class GetDifficulty : WwwRequest {
    public DifficultyModelComponent Component;

    public GetDifficulty(long id) {
        urlData.Add("difficulties");
        urlData.Add(id.ToString());
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<DifficultyModelComponent>(result);
    }
}
