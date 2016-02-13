public class DeleteDifficulty : WwwRequest
{
    public DeleteDifficulty(long difficultyId) {
        urlData.Add("difficulties");
        urlData.Add("delete");
        urlData.Add(difficultyId.ToString());
        postData.Add("", "");
    }

    public override void ParseResult() { }
}
