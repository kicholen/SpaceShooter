using System;
using System.Collections.Generic;

public class LevelService : ILevelService {
    IWwwService wwwService;
    EventService eventService;

    public LevelService(IWwwService wwwService, EventService eventService) {
        this.wwwService = wwwService;
        this.eventService = eventService;
    }

    public void CreateNewLevel(Action<LevelModelComponent> onLevelCreated) {
        wwwService.Send<CreateLevel>(new CreateLevel(), (request) => { onLevelCreated(request.Component); }, onRequestFailed);
    }

    public void DeleteLevel(long id, Action onLevelDeleted) {
        wwwService.Send<DeleteLevel>(new DeleteLevel(id), (request) => onLevelDeleted(), onRequestFailed);
    }

    public void LoadLevelById(long id, Action<LevelModelComponent> onLevelLoaded) {
        wwwService.Send<GetLevel>(new GetLevel(id), (request) => { onLevelLoaded(request.Component); }, onRequestFailed);
    }

    public void LoadLevelIds(Action<List<string>> onLevelIdsLoaded) {
        wwwService.Send<GetLevelIds>(new GetLevelIds(), (request) => { onLevelIdsLoaded(request.LevelIds); }, onRequestFailed);
    }

    public void UpdateLevel(LevelModelComponent component, Action onLevelUpdated) {
        wwwService.Send<UpdateLevel>(new UpdateLevel(component), (request) => onLevelUpdated(), onRequestFailed);
    }

    void onRequestFailed(string message) {
        eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent(message));
    }
}