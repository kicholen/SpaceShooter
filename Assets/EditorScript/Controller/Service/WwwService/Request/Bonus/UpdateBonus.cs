using Newtonsoft.Json;

public class UpdateBonus : WwwRequest {
    public UpdateBonus(BonusModelComponent component) {
        urlData.Add("bonuses");
        urlData.Add("update");
        postData.Add("data", JsonConvert.SerializeObject(component, Formatting.None, new JsonConverter[] { new Vector2Converter() }));
    }

    public override void ParseResult() { }
}
