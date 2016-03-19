using RSG;
using System;
using System.Collections.Generic;

public interface IPathService
{
    IPromise LoadPaths();
    PathModel TryToGetPath(string id);
    List<string> GetPathNames();
    void LoadPathIds(Action<Dictionary<long, string>> onPathsLoaded);
    void LoadPathById(long id, Action<PathModel> onPathLoaded);
    void CreateNewPath(Action<PathModel> onPathCreated);
    void UpdatePath(PathModel component, Action onPathUpdated);
    void DeletePath(long id, Action onPathDeleted);
}