using Entitas;
using UnityEngine;

public class TrailSystem : IExecuteSystem, ISetPool {
    Group group;
    Group time;

    public void SetPool(Pool pool) {
        group = pool.GetGroup(Matcher.AllOf(Matcher.GameObject, Matcher.Trail));
        time = pool.GetGroup(Matcher.Time);
    }

    public void Execute() {
        float modificator = time.GetSingleEntity().time.modificator;
        if (modificator == 0.0f)
            return;
        foreach (Entity e in group.GetEntities())
            update(e, modificator);
    }

    void update(Entity e, float modificator) {
        TrailComponent component = e.trail;
        GameObject gameObject = e.gameObject.gameObject;
        gameObject.GetComponent<TrailRenderer>().time = component.time / modificator;
    }
}

