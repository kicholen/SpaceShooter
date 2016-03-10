using Entitas;
using System.Collections.Generic;

public class FirstBossStageEntry : BossStage
{
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }
    public int Count { get { return 1; } }

    public FirstBossStageEntry()
    {
        timeLimit = 2.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
        if (!e.hasTween)
        {
            e.AddTween(false, new List<Tween>());
            e.tween.AddTween(e.position, EaseTypes.quadIn, PositionAccessorType.X, timeLimit)
                .From(e.position.pos.x + 5.0f)
                .To(e.position.pos.x)
                .BlockClear();
            e.tween.AddTween(e.position, EaseTypes.quadOut, PositionAccessorType.Y, timeLimit)
                .From(e.position.pos.y - 4.0f)
                .To(e.position.pos.y + Config.CAMERA_SPEED * timeLimit)
                .BlockClear();
        }
    }
}
