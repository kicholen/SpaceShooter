public class DeletePath : WwwRequest {

    public DeletePath(long pathId) {
        urlData.Add("paths");
        urlData.Add("delete");
        urlData.Add(pathId.ToString());
        postData.Add("", "");
    }

    public override void ParseResult() { }
}
