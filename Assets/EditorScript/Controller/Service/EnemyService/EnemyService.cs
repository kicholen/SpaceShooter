using Entitas;
using RSG;
using System;
using System.Collections.Generic;
using System.Linq;

public class EnemyService : IEnemyService {
    IWwwService wwwService;
    EventService eventService;
    Pool pool;

    List<EnemyModelComponent> enemies;

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

    public void LoadEnemyById(long id, Action<EnemyModelComponent> onEnemyLoaded) {
        wwwService.Send<GetEnemy>(new GetEnemy(id), (request) => { onEnemyLoaded(request.Component); }, onRequestFailed);
    }

    public void CreateNewEnemy(Action<EnemyModelComponent> onEnemyCreated) {
        wwwService.Send<CreateEnemy>(new CreateEnemy(), (request) => { onEnemyCreated(request.Component); }, onRequestFailed);
    }

    public void UpdateEnemy(EnemyModelComponent component, Action onEnemyUpdated) {
        wwwService.Send<UpdateEnemy>(new UpdateEnemy(component), (request) => onEnemyUpdated(), onRequestFailed);
    }

    public void DeleteEnemy(long id, Action onEnemyDeleted) {
        wwwService.Send<DeleteEnemy>(new DeleteEnemy(id), (request) => onEnemyDeleted(), onRequestFailed);
    }

    void onRequestFailed(string message) {
        eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent(message));
    }

    void replaceOrAddEnemies(List<EnemyModelComponent> enemies)
    {
        Entity[] entities = pool.GetGroup(Matcher.EnemyModel).GetEntities();

        foreach (EnemyModelComponent enemy in enemies) {
            bool found = false;
            foreach (Entity e in entities) {
                if (enemy.type == e.enemyModel.type) {
                    e.ReplaceEnemyModel(enemy.id, enemy.type, enemy.resource, enemy.weapon, enemy.amount, enemy.time, enemy.spawnDelay,
                        enemy.weaponResource, enemy.velocity, enemy.angle, enemy.waves, enemy.angleOffset, enemy.startVelocity, enemy.followDelay,
                        enemy.selfDestructionDelay, enemy.timeDelay, enemy.delay, enemy.randomPositionOffsetX, enemy.faceDirection, enemy.shakeCamera,
                        enemy.randomRotation);
                    found = true;
                }
            }
            if (!found)
                pool.CreateEntity()
                    .AddComponent(ComponentIds.EnemyModel, enemy);
        }
    }
}