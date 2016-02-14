using Newtonsoft.Json;

public class CreateLanguage : WwwRequest
{
    public LanguageModel Model;

    public CreateLanguage() {
        urlData.Add("languages");
        urlData.Add("new");
    }

    public override void ParseResult() {
        Model = JsonConvert.DeserializeObject<LanguageModel>(result);
    }
}
