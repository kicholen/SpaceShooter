using System;
using System.Collections.Generic;

public class EnemyService : IEnemyService {
    IWwwService wwwService;
    EventService eventService;

    public EnemyService(IWwwService wwwService, EventService eventService) {
        this.wwwService = wwwService;
        this.eventService = eventService;
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
}