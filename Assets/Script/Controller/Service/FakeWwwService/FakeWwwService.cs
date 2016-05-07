using System;

public class FakeWwwService : IWwwService
{
    BonusFaker bonus;
    DifficultyFaker difficulty;
    EnemyFaker enemy;
    LevelFaker level;
    PathFaker path;

    public FakeWwwService()
    {
        bonus = new BonusFaker();
        difficulty = new DifficultyFaker();
        enemy = new EnemyFaker();
        level = new LevelFaker();
        path = new PathFaker();
    }

    public void Send<T>(T request, Action<T> onSuccess, Action<string> onFailure) where T : WwwRequest
    {
        onSuccess(GetResponse(request));
    }

    T GetResponse<T>(T request) where T : WwwRequest
    {
        CheckBonus(request);
        CheckDifficulty(request);
        CheckEnemy(request);
        CheckLevel(request);
        CheckPath(request);
        return request;
    }

    void CheckBonus<T>(T request) where T : WwwRequest
    {
        if (request is CreateBonus)
            bonus.Create(request as CreateBonus);
        if (request is DeleteBonus)
            bonus.Delete(request as DeleteBonus);
        else if (request is GetBonus)
            bonus.Get(request as GetBonus);
        else if (request is GetBonusIds)
            bonus.GetIds(request as GetBonusIds);
        else if (request is GetBonuses)
            bonus.GetAll(request as GetBonuses);
        else if (request is UpdateBonus)
            bonus.Update(request as UpdateBonus);
    }

    void CheckDifficulty<T>(T request) where T : WwwRequest
    {
        if (request is CreateDifficulty)
            difficulty.Create(request as CreateDifficulty);
        if (request is DeleteDifficulty)
            difficulty.Delete(request as DeleteDifficulty);
        else if (request is GetDifficulty)
            difficulty.Get(request as GetDifficulty);
        else if (request is GetDifficultyIds)
            difficulty.GetIds(request as GetDifficultyIds);
        else if (request is GetDifficulties)
            difficulty.GetAll(request as GetDifficulties);
        else if (request is UpdateDifficulty)
            difficulty.Update(request as UpdateDifficulty);
    }

    void CheckEnemy<T>(T request) where T : WwwRequest
    {
        if (request is CreateEnemy)
            enemy.Create(request as CreateEnemy);
        if (request is DeleteEnemy)
            enemy.Delete(request as DeleteEnemy);
        else if (request is GetEnemy)
            enemy.Get(request as GetEnemy);
        else if (request is GetEnemyIds)
            enemy.GetIds(request as GetEnemyIds);
        else if (request is GetEnemies)
            enemy.GetAll(request as GetEnemies);
        else if (request is UpdateEnemy)
            enemy.Update(request as UpdateEnemy);
    }

    void CheckLevel<T>(T request) where T : WwwRequest
    {
        if (request is CreateLevel)
            level.Create(request as CreateLevel);
        if (request is DeleteLevel)
            level.Delete(request as DeleteLevel);
        else if (request is GetLevel)
            level.Get(request as GetLevel);
        else if (request is GetLevelIds)
            level.GetIds(request as GetLevelIds);
        else if (request is GetLevels)
            level.GetAll(request as GetLevels);
        else if (request is UpdateLevel)
            level.Update(request as UpdateLevel);
    }

    void CheckPath<T>(T request) where T : WwwRequest
    {
        if (request is CreatePath)
            path.Create(request as CreatePath);
        if (request is DeletePath)
            path.Delete(request as DeletePath);
        else if (request is GetPath)
            path.Get(request as GetPath);
        else if (request is GetPathIds)
            path.GetIds(request as GetPathIds);
        else if (request is GetPaths)
            path.GetAll(request as GetPaths);
        else if (request is UpdatePath)
            path.Update(request as UpdatePath);
    }
}
