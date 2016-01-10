using System;
using System.Collections.Generic;

public interface IPathService
{
    void LoadPathIds(Action<List<string>> onPathsLoaded);
    void LoadPathById(string pathId, Action<PathModelComponent> onPathLoaded);
    void CreateNewPath(Action<PathModelComponent> onPathCreated);
    void UpdatePath(PathModelComponent component, Action onPathUpdated);
}