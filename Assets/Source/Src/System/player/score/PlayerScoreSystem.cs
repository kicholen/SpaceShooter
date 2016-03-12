using Entitas;
using UnityEngine.UI;

public class PlayerScoreSystem : IExecuteSystem, ISetPool
{
    Group group;
    Group killInfo;
    Group score;

    public void SetPool(Pool pool)
    {
        group = pool.GetGroup(Matcher.AllOf(Matcher.PlayerScore, Matcher.GameObject));
        killInfo = pool.GetGroup(Matcher.AllOf(Matcher.KillInfo, Matcher.GameObject));
        score = pool.GetGroup(Matcher.Score);
    }

    public void Execute()
    {
        ScoreComponent scoreComponent = score.GetSingleEntity().score;
        foreach (Entity e in group.GetEntities())
            updateScore(e, scoreComponent);
    }

    void updateScore(Entity e, ScoreComponent scoreComponent)
    {
        setInfoIfNotExist(scoreComponent.multiplierCount);
        e.gameObject.gameObject.GetComponent<Text>().text = scoreComponent.score.ToString();
    }

    void setInfoIfNotExist(int count)
    {
        killInfo.GetSingleEntity().gameObject.gameObject.GetComponent<Text>().text = getKillInfoText(count);
    }

    string getKillInfoText(int count)
    {
        if (count > 20)
            return "MonsterKill";
        if (count > 15)
            return "UltraKill";
        if (count > 10)
            return "SuperKill";
        if (count > 5)
            return "Neat";

        return "";
    }
}
