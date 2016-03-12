using System;
using System.Collections.Generic;

public class AnalyticsService : IAnalyticsService
{
    List<IAnalyticsStrategy> strategies;
    private ISettingsService settingsService;

    public AnalyticsService(ISettingsService settingsService)
    {
        this.settingsService = settingsService;
        createStrategies();
    }

    void createStrategies()
    {
        strategies = new List<IAnalyticsStrategy>();
        strategies.Add(new GameAnalyticsStrategy());
    }

    public void GameStart(string levelName)
    {
        strategies.ForEach(strategy => strategy.GameStart(levelName));
    }

    public void GameCoins(string levelName, int count)
    {
        strategies.ForEach(strategy => strategy.GameCoins(levelName, count));
    }

    public void GameFail(string levelName, int score)
    {
        strategies.ForEach(strategy => strategy.GameFail(levelName, score));
    }

    public void GameComplete(string levelName, int score)
    {
        strategies.ForEach(strategy => strategy.GameComplete(levelName, score));
    }

    public void GameShips(string levelName, int count)
    {
        strategies.ForEach(strategy => strategy.GameShips(levelName, count));

    }

    public void GameBonuses(string levelName, int count)
    {
        strategies.ForEach(strategy => strategy.GameBonuses(levelName, count));
    }
}