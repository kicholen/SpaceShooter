using System;
using System.Collections.Generic;

public interface IEnemyService {
    void LoadEnemies(Action onLoaded);
    void LoadEnemyIds(Action<Dictionary<long, string>> onEnemiesLoaded);
    void LoadEnemyById(long id, Action<EnemyModelComponent> onEnemyLoaded);
    void CreateNewEnemy(Action<EnemyModelComponent> onEnemyCreated);
    void UpdateEnemy(EnemyModelComponent component, Action onEnemyUpdated);
    void DeleteEnemy(long id, Action onEnemyDeleted);
}