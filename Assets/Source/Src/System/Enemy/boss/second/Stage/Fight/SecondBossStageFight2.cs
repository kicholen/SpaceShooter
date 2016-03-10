using Entitas;
using System.Collections.Generic;

public class SecondBossStageFight2 : BossStage
{
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }

    public SecondBossStageFight2()
    {
        timeLimit = 0.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
        List<Entity> children = e.parent.children;

        foreach (Entity child in children)
        {
            child.RemoveChild();
            child.RemoveRelativePosition();
            child.AddHealth(0)
                .AddVelocityLimit(3.0f)
                .AddFindTarget(CollisionTypes.Player)
                .IsTargetMissile(true);
        }
        children.Clear();
    }
}
