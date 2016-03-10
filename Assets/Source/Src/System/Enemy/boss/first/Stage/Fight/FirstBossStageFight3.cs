using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossStageFight3 : BossStage
{
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }

    public FirstBossStageFight3()
    {
        timeLimit = 6.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
        List<Entity> children = e.parent.children;
        bool isFirst = true;
        foreach (Entity child in children)
        {
            child.RemoveChild();
            child.RemoveRelativePosition();
            child.AddVelocity(new Vector2(isFirst ? 0.2f : -0.2f, -0.5f));
            child.AddTween(false, new List<Tween>());
            child.tween.AddTween(child.laserSpawner, EaseTypes.quadOut, LaserSpawnerAccessorType.ANGLE, 3.0f)
                .From(child.laserSpawner.angle)
                .To(isFirst ? 360.0f : -360.0f)
                .PingPong();

            isFirst = false;
        }
        children.Clear();
    }
}
