using Newtonsoft.Json;

public class GetLanguage : WwwRequest
{
    public LanguageModel Model;

    public GetLanguage(long id)
    {
        urlData.Add("languages");
        urlData.Add(id.ToString());
    }

    public override void ParseResult()
    {
        Model = JsonConvert.DeserializeObject<LanguageModel>(result);
    }
}
