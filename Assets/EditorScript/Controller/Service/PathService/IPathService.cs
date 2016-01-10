using System;
using System.Collections.Generic;

public interface IPathService
{
    void LoadPathIds(Action<List<string>> onPathsLoaded);
    void LoadPathById(string pathId, Action<PathModelComponent> onPathLoaded);
}