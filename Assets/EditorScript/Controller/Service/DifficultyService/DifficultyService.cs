using Entitas;
using System;
using System.Collections.Generic;

public class DifficultyService : IDifficultyService
{
    IWwwService wwwService;
    EventService eventService;
    Pool pool;

    List<DifficultyModelComponent> difficulties;

    public DifficultyService(Pool pool, IWwwService wwwService, EventService eventService) {
        this.pool = pool;
        this.wwwService = wwwService;
        this.eventService = eventService;
    }

    public void CreateNewDifficulty(Action<DifficultyModelComponent> onCreated) {
        wwwService.Send<CreateDifficulty>(new CreateDifficulty(), (request) => { onCreated(request.Component); }, onRequestFailed);
    }

    public void DeleteDifficulty(long id, Action onDeleted) {
        wwwService.Send<DeleteDifficulty>(new DeleteDifficulty(id), (request) => onDeleted(), onRequestFailed);
    }

    public void LoadDifficulties(Action onLoaded) {
        wwwService.Send<GetDifficulties>(new GetDifficulties(), (request) => {
            difficulties = request.Difficulties;
            replaceOrAddDifficulties(difficulties);
            onLoaded();
        }, onRequestFailed);
    }

    public void LoadDifficultyById(long id, Action<DifficultyModelComponent> onLoaded) {
        wwwService.Send<GetDifficulty>(new GetDifficulty(id), (request) => { onLoaded(request.Component); }, onRequestFailed);
    }

    public void LoadDifficultyIds(Action<Dictionary<long, string>> onLoaded) {
        wwwService.Send<GetDifficultyIds>(new GetDifficultyIds(), (request) => { onLoaded(request.DifficultyIds); }, onRequestFailed);
    }

    public void UpdateDifficulty(DifficultyModelComponent component, Action onUpdated) {
        wwwService.Send<UpdateDifficulty>(new UpdateDifficulty(component), (request) => onUpdated(), onRequestFailed);
    }

    void onRequestFailed(string message) {
        eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent(message));
    }

    void replaceOrAddDifficulties(List<DifficultyModelComponent> difficulties) {
        Entity[] entities = pool.GetGroup(Matcher.DifficultyModel).GetEntities();

        foreach (DifficultyModelComponent difficulty in difficulties) {
            bool found = false;
            foreach (Entity e in entities) {
                if (difficulty.type == e.difficultyModel.type) {
                    e.ReplaceDifficultyModel(difficulty.id, difficulty.type, difficulty.hpBoostPercent, difficulty.dmgBoostPercent, difficulty.missileSpeedBoostPercent);
                    found = true;
                }
            }
            if (!found)
                pool.CreateEntity()
                    .AddComponent(ComponentIds.DifficultyModel, difficulty);
        }
    }
}
