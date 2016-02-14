using Newtonsoft.Json;

public class UpdateLanguage : WwwRequest
{
    public UpdateLanguage(LanguageModel model) {
        urlData.Add("languages");
        urlData.Add("update");
        postData.Add("data", JsonConvert.SerializeObject(model, Formatting.None, new JsonConverter[] { new Vector2Converter() }));
    }

    public override void ParseResult() { }
}
