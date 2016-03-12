using UnityEngine;
using Entitas;

public class TimeSystem : IExecuteSystem, IInitializeSystem, ISetPool {
	Group group;
	Pool pool;

    public void SetPool(Pool pool)
    {
        this.pool = pool;
        group = pool.GetGroup(Matcher.Time);
    }

    public void Initialize() {
		pool.CreateEntity()
			.AddTime(Time.deltaTime, Time.deltaTime, Time.time, 1.0f, true);
	}
	
	public void Execute() {
		Entity e = group.GetSingleEntity();
		TimeComponent time = e.time;
		float deltaTime = Time.deltaTime;
		time.deltaTime = deltaTime;
		time.gameDeltaTime = deltaTime * time.modificator;
        time.gameTime += time.gameDeltaTime;
	}
}
