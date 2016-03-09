using Entitas;
using UnityEngine;

public class FirstBossStage2 : BossStage
{
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }

    public FirstBossStage2()
    {
        timeLimit = 4.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
        bool isFirst = false;
        foreach (Entity child in e.parent.children)
        {
            if (!child.hasLaserSpawner)
                child.AddLaserSpawner(5.0f, 2.0f, 2.0f, isFirst ? 90.0f : -90.0f, new Vector2(), CollisionTypes.Enemy, 1, Resource.EnemyLaser, null);
            isFirst = true;
        }
    }
}
