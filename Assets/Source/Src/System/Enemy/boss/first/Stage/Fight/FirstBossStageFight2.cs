using Entitas;
using UnityEngine;

public class FirstBossStageFight2 : BossStage
{
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }

    public FirstBossStageFight2()
    {
        timeLimit = 4.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
        bool isFirst = false;
        foreach (Entity child in e.parent.children)
        {
            if (!child.hasLaserSpawner)
                child.AddLaserSpawner(5.0f, 0.1f, 0.1f, isFirst ? 90.0f : -90.0f, new Vector2(), CollisionTypes.Enemy, 1, Resource.EnemyLaser, null);
            else
                updateLaserHeight(deltaTime, child.laserSpawner);
            isFirst = true;
        }
    }

    private void updateLaserHeight(float deltaTime, LaserSpawnerComponent laserSpawner)
    {
        laserSpawner.height += deltaTime;
        laserSpawner.maxHeight += deltaTime;
    }
}
