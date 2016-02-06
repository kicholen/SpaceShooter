public class DeleteEnemy : WwwRequest {

    public DeleteEnemy(long enemyId) {
        urlData.Add("ships");
        urlData.Add("delete");
        urlData.Add(enemyId.ToString());
        postData.Add("", "");
    }

    public override void ParseResult() { }
}
