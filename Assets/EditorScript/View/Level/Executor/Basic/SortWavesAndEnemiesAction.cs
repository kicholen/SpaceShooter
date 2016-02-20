public class SortWavesAndEnemiesAction : ILevelAction
{
    public void Execute(LevelModelComponent component)
    {
        component.waves.Sort((first, second) => first.spawnBarrier.CompareTo(second.spawnBarrier));
        component.enemies.Sort((first, second) => first.spawnBarrier.CompareTo(second.spawnBarrier));
    }
}
