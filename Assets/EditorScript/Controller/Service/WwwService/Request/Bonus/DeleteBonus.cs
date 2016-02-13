public class DeleteBonus : WwwRequest
{
    public DeleteBonus(long enemyId) {
        urlData.Add("bonuses");
        urlData.Add("delete");
        urlData.Add(enemyId.ToString());
        postData.Add("", "");
    }

    public override void ParseResult() { }
}
