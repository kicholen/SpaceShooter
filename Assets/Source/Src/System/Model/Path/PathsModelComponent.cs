using Entitas;
using System.Collections.Generic;

public class PathsModelComponent : IComponent
{
    public Dictionary<string, PathModel> map;
}