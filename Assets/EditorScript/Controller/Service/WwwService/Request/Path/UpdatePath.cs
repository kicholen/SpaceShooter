using Newtonsoft.Json;
using UnityEngine;

public class UpdatePath : WwwRequest {

    public UpdatePath(PathModel component) {
        urlData.Add("paths");
        urlData.Add("update");
        postData.Add("data", JsonConvert.SerializeObject(component, Formatting.None, new JsonConverter[] { new Vector2Converter() }));
    }

    public override void ParseResult() { }
}
