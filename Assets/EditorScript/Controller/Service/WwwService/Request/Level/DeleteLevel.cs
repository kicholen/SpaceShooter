public class DeleteLevel : WwwRequest {

    public DeleteLevel(long levelId) {
        urlData.Add("levels");
        urlData.Add("delete");
        urlData.Add(levelId.ToString());
        postData.Add("", "");
    }

    public override void ParseResult() { }
}
