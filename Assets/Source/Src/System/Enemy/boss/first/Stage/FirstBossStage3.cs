using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossStage3 : BossStage
{
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }

    public FirstBossStage3()
    {
        timeLimit = 6.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
        List<Entity> children = e.parent.children;
        foreach (Entity child in children)
        {
            child.RemoveChild();
            child.RemoveRelativePosition();
            child.AddVelocity(new Vector2(0.0f, -2.0f));
        }
        children.Clear();
    }
}
