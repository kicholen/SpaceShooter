using Entitas;
using RSG;
using System;
using System.Collections.Generic;
using System.Linq;

public class EnemyService : IEnemyService {
    IWwwService wwwService;
    EventService eventService;
    Pool pool;

    List<EnemyModel> enemies;

    public EnemyService(Pool pool, IWwwService wwwService, EventService eventService) {
        this.pool = pool;
        this.wwwService = wwwService;
        this.eventService = eventService;
    }

    public IPromise LoadEnemies() {
        Promise promise = new Promise();
        wwwService.Send<GetEnemies>(new GetEnemies(), (request) => {
            enemies = request.Enemies;
            replaceOrAddEnemies(enemies);
            promise.Resolve();
        }, (error) => {
            promise.Reject(new Exception(error));
        });
        return promise;
    }

    public List<string> GetEnemyNames()
    {
        return enemies.Select(cmp => cmp.type.ToString()).ToList<string>();
    }

    public void LoadEnemyIds(Action<Dictionary<long, string>> onEnemiesLoaded) {
        wwwService.Send<GetEnemyIds>(new GetEnemyIds(), (request) => { onEnemiesLoaded(request.EnemyIds); }, onRequestFailed);
    }

    public void LoadEnemyById(long id, Action<EnemyModel> onEnemyLoaded) {
        wwwService.Send<GetEnemy>(new GetEnemy(id), (request) => { onEnemyLoaded(request.Component); }, onRequestFailed);
    }

    public void CreateNewEnemy(Action<EnemyModel> onEnemyCreated) {
        wwwService.Send<CreateEnemy>(new CreateEnemy(), (request) => { onEnemyCreated(request.Component); }, onRequestFailed);
    }

    public void UpdateEnemy(EnemyModel component, Action onEnemyUpdated) {
        wwwService.Send<UpdateEnemy>(new UpdateEnemy(component), (request) => onEnemyUpdated(), onRequestFailed);
    }

    public void DeleteEnemy(long id, Action onEnemyDeleted) {
        wwwService.Send<DeleteEnemy>(new DeleteEnemy(id), (request) => onEnemyDeleted(), onRequestFailed);
    }

    void onRequestFailed(string message) {
        eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent(message));
    }

    void replaceOrAddEnemies(List<EnemyModel> enemies)
    {
        Dictionary<int, EnemyModel> map = pool.GetGroup(Matcher.EnemiesModel).GetSingleEntity().enemiesModel.map;
        foreach (EnemyModel enemy in enemies)
            map[enemy.type] = enemy;
    }
}