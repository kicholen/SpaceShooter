using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollisionSystem : IReactiveSystem {
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Damage, Matcher.ShieldCollision).OnEntityAdded(); } }

    public void Execute(List<Entity> entities) {
        foreach (Entity e in entities)
            onCollision(e);
    }

    void onCollision(Entity e) {
        ShieldCollisionComponent component = e.shieldCollision;
        GameObject gameObject = e.gameObject.gameObject;
        CollisionScriptExtended collision = gameObject.GetComponent<CollisionScriptExtended>();
        
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Vector2 extents = renderer.sprite.bounds.extents;
        Vector2 bottomLeftCornerPosition = (Vector2)gameObject.transform.position - extents;
        Vector2 uvPosition = collision.Position.Dequeue() - bottomLeftCornerPosition;
        component.collisionsPosition.Enqueue(new Vector2(Mathf.Min(1.0f, Mathf.Max(0.1f, uvPosition.x)), Mathf.Min(1.0f, Mathf.Max(0.1f, uvPosition.y))));
        collision.Position.Clear();
    }
}

