using System;
using System.Collections.Generic;

public class PathService : IPathService {

    IWwwService wwwService;

    public PathService(IWwwService wwwService) {
        this.wwwService = wwwService;
    }

    public void LoadPathIds(Action<List<string>> onPathsLoaded) {
        wwwService.Send<GetPathIds>(new GetPathIds(), (request) => { onPathsLoaded(request.PathIds); }, onRequestFailed);
    }

    public void LoadPathById(string pathId, Action<PathModelComponent> onPathLoaded) {
        wwwService.Send<GetPath>(new GetPath(pathId), (request) => { onPathLoaded(request.Component); }, onRequestFailed);
    }

    public void CreateNewPath(Action<PathModelComponent> onPathCreated) {
        wwwService.Send<CreatePath>(new CreatePath(), (request) => { onPathCreated(request.Component); }, onRequestFailed);
    }

    public void UpdatePath(PathModelComponent component, Action onPathUpdated) {
        wwwService.Send<UpdatePath>(new UpdatePath(component), (request) => onPathUpdated(), onRequestFailed);
    }

    void onRequestFailed(string message) {
        throw new NotImplementedException(message);
    }
}