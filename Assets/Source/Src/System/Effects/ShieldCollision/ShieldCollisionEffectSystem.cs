using Entitas;
using UnityEngine;

public class ShieldCollisionEffectSystem : IExecuteSystem, ISetPool {
    const float maxPower = 0.3f;
    Group group;
    Group time;

    public void SetPool(Pool pool) {
        group = pool.GetGroup(Matcher.AllOf(Matcher.GameObject, Matcher.ShieldCollision));
        time = pool.GetGroup(Matcher.Time);
    }

    public void Execute() {
        float deltaTime = time.GetSingleEntity().time.gameDeltaTime;
        foreach (Entity e in group.GetEntities())
            update(e, deltaTime);
    }

    void update(Entity e, float deltaTime) {
        ShieldCollisionComponent component = e.shieldCollision;
        GameObject gameObject = e.gameObject.gameObject;
        if (component.time > 0.0f)
            setShaderPower(component, gameObject, deltaTime);
        else if (component.collisionsPosition.Count > 0)
            setHitVectorAndRestTime(component, gameObject);
        else
            getShaderMaterial(gameObject).SetFloat("_Power", 0.0f);
    }

    void setHitVectorAndRestTime(ShieldCollisionComponent component, GameObject gameObject) {
        setHitVector(component, gameObject);
        component.time = component.duration;
    }

    void setShaderPower(ShieldCollisionComponent component, GameObject gameObject, float deltaTime) {
        Material shaderMaterial = getShaderMaterial(gameObject);
        shaderMaterial.SetFloat("_Power", maxPower * component.time / component.duration);

        component.time -= deltaTime;
    }

    void setHitVector(ShieldCollisionComponent component, GameObject gameObject) {
        Vector2 uvPosition = component.collisionsPosition.Dequeue();
        getShaderMaterial(gameObject).SetVector("_HitVector", uvPosition);
    }

    Material getShaderMaterial(GameObject gameObject) {
        return gameObject.GetComponent<SpriteRenderer>().material;
    }
}

