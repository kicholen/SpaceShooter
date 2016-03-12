using System;
using GameAnalyticsSDK;

public class GameAnalyticsStrategy : IAnalyticsStrategy
{
    const string COINS_CURRENCY = "Coins";
    const string BONUSES_CURRENCY = "Bonuses";

    const string GAME_ITEM_TYPE = "Gameplay";

    public void GameCoins(string levelName, int count)
    {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, COINS_CURRENCY, count, GAME_ITEM_TYPE, levelName);
    }

    public void GameStart(string levelName)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, levelName);
    }

    public void GameFail(string levelName, int score)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, levelName, score);
    }

    public void GameComplete(string levelName, int score)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, levelName, score);
    }

    public void GameShips(string levelName, int count)
    {
        GameAnalytics.NewDesignEvent("Game:ShipsDestroyed:" + levelName, count);
    }

    public void GameBonuses(string levelName, int count)
    {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, BONUSES_CURRENCY, count, GAME_ITEM_TYPE, levelName);
    }
}
