using Newtonsoft.Json;

public class GetEnemy : WwwRequest {

    public EnemyModel Component;

    public GetEnemy(long id) {
        urlData.Add("ships");
        urlData.Add(id.ToString());
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<EnemyModel>(result);
    }
}
