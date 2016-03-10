using Entitas;

public class SecondBossStageFight3 : BossStage
{
    float timeLimit;

    public float TimeLimit { get { return timeLimit; } }

    public SecondBossStageFight3()
    {
        timeLimit = 5.0f;
    }

    public void Update(Entity e, float deltaTime)
    {
    }
}
