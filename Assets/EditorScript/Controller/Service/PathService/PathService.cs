using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

public class PathService : IPathService {
    Pool pool;
    IWwwService wwwService;
    EventService eventService;

    List<PathModelComponent> paths;

    public PathService(Pool pool, IWwwService wwwService, EventService eventService) {
        this.pool = pool;
        this.wwwService = wwwService;
        this.eventService = eventService;
    }

    public void LoadPathIds(Action<Dictionary<long, string>> onPathsLoaded) {
        wwwService.Send<GetPathIds>(new GetPathIds(), (request) => { onPathsLoaded(request.PathIds); }, onRequestFailed);
    }

    public void LoadPaths(Action onPathsLoaded) {
        wwwService.Send<GetPaths>(new GetPaths(), (request) => {
            paths = request.Paths;
            replaceOrAddPaths(paths);
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

    public List<string> GetPathNames() {
        return paths.Select(cmp => cmp.name).ToList<string>();
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

    void replaceOrAddPaths(List<PathModelComponent> paths) {
        Entity[] entities = pool.GetGroup(Matcher.PathModel).GetEntities();

        foreach (PathModelComponent path in paths) {
            bool found = false;
            foreach (Entity e in entities) {
                if (path.name == e.pathModel.name) {
                    e.ReplacePathModel(path.id, path.name, path.points);
                    found = true;
                }
            }
            if (!found)
                pool.CreateEntity()
                    .AddComponent(ComponentIds.PathModel, path);
        }
    }
}