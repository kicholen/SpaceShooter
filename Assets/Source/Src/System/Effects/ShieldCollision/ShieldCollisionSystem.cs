using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollisionSystem : IReactiveSystem {
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Damage, Matcher.Shield).OnEntityAdded(); } }

    public void Execute(List<Entity> entities) {
        foreach (Entity e in entities) {
            checkCollision(e);
        }
    }

    void checkCollision(Entity e) {
        GameObject gameObject = e.gameObject.gameObject;
        CollisionScriptExtended collision = gameObject.GetComponent<CollisionScriptExtended>();
        
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Vector2 extents = renderer.sprite.bounds.extents;
        Vector2 bottomLeftCornerPosition = (Vector2)gameObject.transform.position - extents;
        Vector2 uvPosition = collision.Position[0] - bottomLeftCornerPosition;

        Material shaderMaterial = renderer.material;
        shaderMaterial.SetVector("_HitVector", new Vector4(Mathf.Max(1.0f, Mathf.Min(0.1f, uvPosition.x)), Mathf.Max(1.0f, Mathf.Min(0.1f, uvPosition.y)), 0));
        shaderMaterial.SetFloat("_Power", 0.3f);

        collision.Position.Clear();
    }
}

