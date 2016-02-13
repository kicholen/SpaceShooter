using Newtonsoft.Json;
using System.Collections.Generic;

public class GetBonuses : WwwRequest {

    public List<BonusModelComponent> Bonuses;

    public GetBonuses() {
        urlData.Add("bonuses/all");
    }

    public override void ParseResult() {
        Bonuses = JsonConvert.DeserializeObject<List<BonusModelComponent>>(result);
    }
}

