using Entitas;
using System.Collections.Generic;

public class FirstBossSystem : IExecuteSystem, ISetPool {
	Pool pool;
	Group group;
	Group time;

    List<BossStage> stages;

    //const float EPSILON = 0.005f;

    public void SetPool(Pool pool) {
		this.pool = pool;
		group = pool.GetGroup(Matcher.FirstBoss);
        time = pool.GetGroup(Matcher.Time);
        createStages();
	}

    public void Execute() {
		float deltaTime = time.GetSingleEntity().time.gameDeltaTime;
        foreach (Entity e in group.GetEntities())
        {
            FirstBossComponent cmp = e.firstBoss;
            cmp.age += deltaTime;
            BossStage stage = stages[cmp.stage];

            stage.Update(e, deltaTime);
            advanceStageIfNeeded(cmp, stage);
        }
    }

    void advanceStageIfNeeded(FirstBossComponent cmp, BossStage stage)
    {
        if (cmp.age > stage.TimeLimit)
        {
            if (stages.Count > (cmp.stage + 1))
                cmp.stage = cmp.stage + 1;
            else
            {
                cmp.age = 0.0f;
                cmp.stage = 0;
            }
        }
    }

    void createStages()
    {
        stages = new List<BossStage>();
        stages.Add(new FirstBossStage1(pool));
        stages.Add(new FirstBossStage2());
        stages.Add(new FirstBossStage3());
    }
}