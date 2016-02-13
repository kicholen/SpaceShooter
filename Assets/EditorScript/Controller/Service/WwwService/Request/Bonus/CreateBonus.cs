using Newtonsoft.Json;

public class CreateBonus : WwwRequest
{
    public BonusModelComponent Component;

    public CreateBonus() {
        urlData.Add("bonuses");
        urlData.Add("new");
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<BonusModelComponent>(result);
    }
}
