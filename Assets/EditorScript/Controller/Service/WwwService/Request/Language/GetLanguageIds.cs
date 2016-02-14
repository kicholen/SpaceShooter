using Newtonsoft.Json;
using System.Collections.Generic;

public class GetLanguageIds : WwwRequest
{
    public Dictionary<long, string> LanguageIds;

    public GetLanguageIds() {
        urlData.Add("languages");
    }

    public override void ParseResult() {
        LanguageIds = JsonConvert.DeserializeObject<Dictionary<long, string>>(result);
    }
}
