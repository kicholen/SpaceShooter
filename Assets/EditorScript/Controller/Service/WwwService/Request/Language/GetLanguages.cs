using Newtonsoft.Json;
using System.Collections.Generic;

public class GetLanguages : WwwRequest
{
    public List<LanguageModel> Languages;

    public GetLanguages() {
        urlData.Add("languages/all");
    }

    public override void ParseResult() {
        Languages = JsonConvert.DeserializeObject<List<LanguageModel>>(result);
    }
}

