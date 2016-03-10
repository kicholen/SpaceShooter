using Entitas;

public class SecondBossStageMovement : BossStage
{
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }

    public SecondBossStageMovement()
    {
        timeLimit = 0.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
        e.tween.tweens.Clear();
        e.tween.AddTween(e.position, EaseTypes.bounceIn, PositionAccessorType.X, 5.0f)
            .From(e.position.pos.x)
            .To(e.position.pos.x < 0.0f ? e.position.pos.x + 5.0f : e.position.pos.x - 5.0f)
            .PingPong();
    }
}