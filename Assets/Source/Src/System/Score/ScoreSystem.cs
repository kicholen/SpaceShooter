using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ScoreSystem : IReactiveSystem, IInitializeSystem, ISetPool
{
    Pool pool;
    Group group;
    Group time;

    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Enemy, Matcher.CollisionDeath).OnEntityAdded(); } }

    public void SetPool(Pool pool)
    {
        this.pool = pool;
        group = pool.GetGroup(Matcher.Score);
        time = pool.GetGroup(Matcher.Time);
    }

    public void Initialize()
    {
        pool.CreateEntity()
            .AddScore(0, 0, Config.SCORE_MULTIPLIER_BASE, 0.0f, Config.SCORE_MULTIPLIER_DURATION);
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
        scoreComponent.multiplier = shouldMultiply ? scoreComponent.multiplier + Config.SCORE_MULTIPLIER_PROGRESS : Config.SCORE_MULTIPLIER_BASE;
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
        return type;
    }
}
