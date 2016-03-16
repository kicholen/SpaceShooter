using Entitas;
using System.Collections.Generic;

public class SecondBossStageEntry : BossStage
{
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }
    public int Count { get { return 1; } }

    public SecondBossStageEntry()
    {
        timeLimit = 0.5f;
    }

    public void Update(Entity e, float deltaTime)
    {
        if (!e.hasTween)
        {
            e.AddTween(true, new List<Tween>());
            e.tween.AddTween(e.position, EaseTypes.quadOut, PositionAccessorType.Y, timeLimit)
                .From(e.position.pos.y + 3.0f)
                .To(e.position.pos.y + GameConfig.CAMERA_SPEED * timeLimit)
                .BlockClear();
        }
    }
}
