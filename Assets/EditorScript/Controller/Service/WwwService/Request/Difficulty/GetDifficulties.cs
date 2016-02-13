using Newtonsoft.Json;
using System.Collections.Generic;

public class GetDifficulties : WwwRequest {
    public List<DifficultyModelComponent> Difficulties;

    public GetDifficulties() {
        urlData.Add("difficulties/all");
    }

    public override void ParseResult() {
        Difficulties = JsonConvert.DeserializeObject<List<DifficultyModelComponent>>(result);
    }
}

