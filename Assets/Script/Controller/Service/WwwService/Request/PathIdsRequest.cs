using System.Collections.Generic;
using UnityEngine;

public class PathIdsRequest : WwwRequest {

    public List<string> ids;

    public PathIdsRequest() {
        urlData.Add("paths");
    }

    public override void ParseResult() {
        Debug.Log(result);
    }
}
