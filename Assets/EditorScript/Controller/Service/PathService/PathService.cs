using Entitas;
using RSG;
using System;
using System.Collections.Generic;
using System.Linq;

public class PathService : IPathService {
    Pool pool;
    IWwwService wwwService;
    EventService eventService;

    List<PathModel> paths;

    public PathService(Pool pool, IWwwService wwwService, EventService eventService) {
        this.pool = pool;
        this.wwwService = wwwService;
        this.eventService = eventService;
    }

    public void LoadPathIds(Action<Dictionary<long, string>> onPathsLoaded) {
        wwwService.Send<GetPathIds>(new GetPathIds(), (request) => { onPathsLoaded(request.PathIds); }, onRequestFailed);
    }

    public IPromise LoadPaths() {
        Promise promise = new Promise();
        wwwService.Send<GetPaths>(new GetPaths(), (request) => {
            paths = request.Paths;
            replaceOrAddPaths(paths);
            promise.Resolve();
         }, (error) => {
             promise.Reject(new Exception(error));
         });
        return promise;
    }

    public PathModel TryToGetPath(string name) {
        PathModel component = null;
        foreach (PathModel path in paths) {
            if (name == path.name)
                component = path;
        }
        return component;
    }

    public List<string> GetPathNames() {
        return paths.Select(cmp => cmp.name).ToList<string>();
    }

    public void LoadPathById(long id, Action<PathModel> onPathLoaded) {
        wwwService.Send<GetPath>(new GetPath(id), (request) => { onPathLoaded(request.Component); }, onRequestFailed);
    }

    public void CreateNewPath(Action<PathModel> onPathCreated) {
        wwwService.Send<CreatePath>(new CreatePath(), (request) => { onPathCreated(request.Component); }, onRequestFailed);
    }

    public void UpdatePath(PathModel component, Action onPathUpdated) {
        wwwService.Send<UpdatePath>(new UpdatePath(component), (request) => onPathUpdated(), onRequestFailed);
    }

    public void DeletePath(long id, Action onPathDeleted) {
        wwwService.Send<DeletePath>(new DeletePath(id), (request) => onPathDeleted(), onRequestFailed);
    }

    void onRequestFailed(string message) {
        eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent(message));
    }

    void replaceOrAddPaths(List<PathModel> paths) {
        Dictionary<string, PathModel> map = pool.GetGroup(Matcher.PathsModel).GetSingleEntity().pathsModel.map;
        foreach (PathModel path in paths)
            map[path.name] = path;
    }
}