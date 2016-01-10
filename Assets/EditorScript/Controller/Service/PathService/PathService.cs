using System;
using System.Collections.Generic;

public class PathService : IPathService {
    IWwwService wwwService;

    public PathService(IWwwService wwwService) {
        this.wwwService = wwwService;
    }

    public void LoadPathIds(Action<List<string>> onPathsLoaded) {
        wwwService.Send<PathIdsRequest>(new PathIdsRequest(), (request) => { onPathsLoaded(request.ids); }, () => { });
    }

    public void LoadPathById(string pathId, Action<PathModelComponent> onPathLoaded) {

    }
}