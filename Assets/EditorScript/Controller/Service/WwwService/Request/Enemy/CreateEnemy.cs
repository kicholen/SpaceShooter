using Newtonsoft.Json;

public class CreateEnemy : WwwRequest {

    public EnemyModelComponent Component;

    public CreateEnemy() {
        urlData.Add("ships");
        urlData.Add("new");
    }

    public override void ParseResult() {
        Component = JsonConvert.DeserializeObject<EnemyModelComponent>(result);
    }
}
