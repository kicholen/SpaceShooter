using Entitas;
using UnityEngine;

public class FirstBossStage1 : BossStage
{
    Pool pool;
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }

    public FirstBossStage1(Pool pool)
    {
        this.pool = pool;
        timeLimit = 2.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
        if (e.parent.children.Count == 0)
        {
            e.parent.children.Add(createLaserChild(e, -1.0f));
            e.parent.children.Add(createLaserChild(e, 1.0f));
        }
    }

    Entity createLaserChild(Entity e, float relativeX)
    {
        return pool.CreateEntity()
            .AddRelativePosition(relativeX, -0.5f)
            .AddPosition(new Vector2(0.0f, 0.0f))
            .AddChild(e)
            .AddResource(Resource.Weapon);
    }
}
