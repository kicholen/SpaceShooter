using Entitas;

public interface BossStage
{
    float TimeLimit { get; }
    void Update(Entity e, float deltaTime);
}
