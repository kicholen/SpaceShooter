using Newtonsoft.Json;
using System.Collections.Generic;

public class GetBonusIds : WwwRequest {

    public Dictionary<long, string> BonusIds;

    public GetBonusIds() {
        urlData.Add("bonuses");
    }

    public override void ParseResult() {
        BonusIds = JsonConvert.DeserializeObject<Dictionary<long, string>>(result);
    }
}
