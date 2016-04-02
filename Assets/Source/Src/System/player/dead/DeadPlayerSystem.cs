using Entitas;
using System.Collections.Generic;

public class DeadPlayerSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.CollisionDeath, Matcher.Player).OnEntityAdded(); } }

	Pool pool;
	Group time;
	Group input;
	Group eventService;
    Group analyticsService;
    Group gameStats;
    Group score;
    Group enemySpawner;

    public void SetPool(Pool pool) {
		this.pool = pool;
		time = pool.GetGroup(Matcher.Time);
		input = pool.GetGroup(Matcher.Input);
		eventService = pool.GetGroup(Matcher.EventService);
		analyticsService = pool.GetGroup(Matcher.AnalyticsService);
		gameStats = pool.GetGroup(Matcher.GameStats);
		score = pool.GetGroup(Matcher.Score);
        enemySpawner = pool.GetGroup(Matcher.EnemySpawner);
    }

	public void Execute(List<Entity> entities)
    {
        freezeInputTimeAndCamera();
        sendGameStats();

        eventService.GetSingleEntity().eventService.dispatcher.Dispatch<GameEndedEvent>(new GameEndedEvent(true));
    }

    void sendGameStats()
    {
        IAnalyticsService service = analyticsService.GetSingleEntity().analyticsService.service;
        GameStatsComponent gameStatsComponent = gameStats.GetSingleEntity().gameStats;
        ScoreComponent scoreComponent = score.GetSingleEntity().score;
        EnemySpawnerComponent enemySpawnerComponent = enemySpawner.GetSingleEntity().enemySpawner;

        service.GameCoins(enemySpawnerComponent.model.name, gameStatsComponent.starsPicked);
        service.GameBonuses(enemySpawnerComponent.model.name, gameStatsComponent.bonusesPicked);
        service.GameShips(enemySpawnerComponent.model.name, gameStatsComponent.shipsDestroyed);
        service.GameFail(enemySpawnerComponent.model.name, scoreComponent.score);
    }

    void freezeInputTimeAndCamera()
    {
        TimeComponent timeComponent = time.GetSingleEntity().time;
        timeComponent.modificator = 0.2f;
        InputComponent inputComponent = input.GetSingleEntity().input;
        inputComponent.isInputBlocked = true;

        pool.CreateEntity()
            .AddCreateCamera(CameraTypes.Static, false);
    }
}
