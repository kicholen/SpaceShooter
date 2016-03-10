using Entitas;

public class FirstBossStageMovement : BossStage
{
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }

    public FirstBossStageMovement()
    {
        timeLimit = 0.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
        e.tween.tweens.Clear();
        e.tween.AddTween(e.position, EaseTypes.bounceIn, PositionAccessorType.X, 5.0f)
            .From(e.position.pos.x)
            .To(e.position.pos.x + 5f)
            .PingPong();
    }
}
