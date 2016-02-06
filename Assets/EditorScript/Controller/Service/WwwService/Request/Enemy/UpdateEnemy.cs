using Newtonsoft.Json;

public class UpdateEnemy : WwwRequest {

    public UpdateEnemy(EnemyModelComponent component) {
        urlData.Add("ships");
        urlData.Add("update");
        postData.Add("data", JsonConvert.SerializeObject(component, Formatting.None, new JsonConverter[] { new Vector2Converter() }));
    }

    public override void ParseResult() { }
}
