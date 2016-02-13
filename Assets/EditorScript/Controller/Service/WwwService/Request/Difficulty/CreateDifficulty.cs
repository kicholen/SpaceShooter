using Newtonsoft.Json;

public class CreateDifficulty : WwwRequest
{
    public DifficultyModelComponent Component;

    public CreateDifficulty() {
        urlData.Add("difficulties");
        urlData.Add("new");
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<DifficultyModelComponent>(result);
    }
}