public class DeleteLanguage : WwwRequest
{
    public DeleteLanguage(long id) {
        urlData.Add("languages");
        urlData.Add("delete");
        urlData.Add(id.ToString());
        postData.Add("", "");
    }

    public override void ParseResult() { }
}
