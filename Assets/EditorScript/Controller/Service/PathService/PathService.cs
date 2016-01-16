using System;
using System.Collections.Generic;

public class PathService : IPathService {
    IWwwService wwwService;
    EventService eventService;

    List<PathModelComponent> paths;

    public PathService(IWwwService wwwService, EventService eventService) {
        this.wwwService = wwwService;
        this.eventService = eventService;
    }

    public void LoadPathIds(Action<List<string>> onPathsLoaded) {
        wwwService.Send<GetPathIds>(new GetPathIds(), (request) => { onPathsLoaded(request.PathIds); }, onRequestFailed);
    }

    public void LoadPaths(Action onPathsLoaded) {
        wwwService.Send<GetPaths>(new GetPaths(), (request) => {
            paths = request.Paths;
            onPathsLoaded();
        }, onRequestFailed);
    }

    public PathModelComponent TryToGetPath(string name) {
        PathModelComponent component = null;
        foreach (PathModelComponent path in paths) {
            if (name == path.name)
                component = path;
        }
        return component;
    }

    public void LoadPathById(long id, Action<PathModelComponent> onPathLoaded) {
        wwwService.Send<GetPath>(new GetPath(id), (request) => { onPathLoaded(request.Component); }, onRequestFailed);
    }

    public void CreateNewPath(Action<PathModelComponent> onPathCreated) {
        wwwService.Send<CreatePath>(new CreatePath(), (request) => { onPathCreated(request.Component); }, onRequestFailed);
    }

    public void UpdatePath(PathModelComponent component, Action onPathUpdated) {
        wwwService.Send<UpdatePath>(new UpdatePath(component), (request) => onPathUpdated(), onRequestFailed);
    }

    public void DeletePath(long id, Action onPathDeleted) {
        wwwService.Send<DeletePath>(new DeletePath(id), (request) => onPathDeleted(), onRequestFailed);
    }

    void onRequestFailed(string message) {
        eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent(message));
    }
}