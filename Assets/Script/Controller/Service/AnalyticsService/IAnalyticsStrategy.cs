public interface IAnalyticsStrategy
{
    void GameShips(string levelName, int count);
    void GameCoins(string levelName, int count);
    void GameBonuses(string levelName, int count);
    void GameStart(string levelName);
    void GameFail(string levelName, int score);
    void GameComplete(string levelName, int score);
}