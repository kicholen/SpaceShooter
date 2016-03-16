using System.Collections.Generic;
using Entitas;

public class ScoreSystem : IReactiveSystem, IInitializeSystem, ISetPool
{
    Pool pool;
    Group group;
    Group time;
    Group enemy;

    Dictionary<int, int> scores;

    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Enemy, Matcher.CollisionDeath).OnEntityAdded(); } }

    public void SetPool(Pool pool)
    {
        this.pool = pool;
        group = pool.GetGroup(Matcher.Score);
        time = pool.GetGroup(Matcher.Time);
        enemy = pool.GetGroup(Matcher.EnemyModel);
    }

    public void Initialize()
    {
        pool.CreateEntity()
            .AddScore(0, 0, GameConfig.SCORE_MULTIPLIER_BASE, 0.0f, GameConfig.SCORE_MULTIPLIER_DURATION);
    }

    public void Execute(List<Entity> entities)
    {
        ScoreComponent scoreComponent = group.GetSingleEntity().score;
        float gameTime = time.GetSingleEntity().time.gameTime;
        bool shouldMultiply = isMultiplierActive(scoreComponent, gameTime);
        setScoreComponent(scoreComponent, shouldMultiply);

        foreach (Entity e in entities)
            addScore(scoreComponent, shouldMultiply, e.enemy.type, gameTime);
    }

    void addScore(ScoreComponent scoreComponent, bool shouldMultiply, int enemyType, float gameTime)
    {
        if (shouldMultiply)
        {
            scoreComponent.score += getMultipliedScore(scoreComponent.multiplier, enemyType);
            scoreComponent.multiplierCount++;
        }
        else
        {
            scoreComponent.score += getScore(enemyType);
            scoreComponent.multiplierCount = 0;
        }
        scoreComponent.time = gameTime;
    }

    void setScoreComponent(ScoreComponent scoreComponent, bool shouldMultiply)
    {
        scoreComponent.multiplier = shouldMultiply ? scoreComponent.multiplier + GameConfig.SCORE_MULTIPLIER_PROGRESS : GameConfig.SCORE_MULTIPLIER_BASE;
    }

    bool isMultiplierActive(ScoreComponent scoreComponent, float gameTime)
    {
        return (scoreComponent.time + scoreComponent.multiplierDuration) > gameTime;
    }

    int getMultipliedScore(float factor, int type)
    {
        return (int)(factor * (float)getScore(type));
    }

    int getScore(int type)
    {
        int value = 0;
        if (getScoreMap().TryGetValue(type, out value))
            return value;
        return 0;
    }

    Dictionary<int, int> getScoreMap()
    {
        if (scores == null)
            createScoreMap();
        return scores;
    }

    void createScoreMap()
    {
        scores = new Dictionary<int, int>();
        foreach (Entity e in enemy.GetEntities())
            scores.Add(e.enemyModel.type, e.enemyModel.score);
    }
}
