using Newtonsoft.Json;

public class GetBonus : WwwRequest {
    public BonusModelComponent Component;

    public GetBonus(long id) {
        urlData.Add("bonuses");
        urlData.Add(id.ToString());
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<BonusModelComponent>(result);
    }
}
