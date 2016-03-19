using RSG;
using System;
using System.Collections.Generic;

public interface IEnemyService {
    IPromise LoadEnemies();
    List<string> GetEnemyNames();
    void LoadEnemyIds(Action<Dictionary<long, string>> onEnemiesLoaded);
    void LoadEnemyById(long id, Action<EnemyModel> onEnemyLoaded);
    void CreateNewEnemy(Action<EnemyModel> onEnemyCreated);
    void UpdateEnemy(EnemyModel component, Action onEnemyUpdated);
    void DeleteEnemy(long id, Action onEnemyDeleted);
}