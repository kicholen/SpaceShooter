using Entitas;
using System.Collections.Generic;

public class EnemiesModelComponent : IComponent
{
    public Dictionary<int, EnemyModel> map;
}