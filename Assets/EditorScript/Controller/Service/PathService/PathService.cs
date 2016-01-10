using System;
using System.Collections.Generic;
using UnityEngine;

public class PathService : IPathService {

    IWwwService wwwService;

    public PathService(IWwwService wwwService) {
        this.wwwService = wwwService;
    }

    public void LoadPathIds(Action<List<string>> onPathsLoaded) {
        wwwService.Send<GetPathIds>(new GetPathIds(), (request) => { onPathsLoaded(request.PathIds); }, onRequestFailed);
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
        throw new NotImplementedException(message);
    }
}