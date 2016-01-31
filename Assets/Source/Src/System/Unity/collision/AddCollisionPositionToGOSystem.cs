using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddCollisionPositionToGOSystem : IReactiveSystem {
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.GameObject, Matcher.CollisionPosition).OnEntityAdded(); } }

    public void Execute(List<Entity> entities) {
        for (int i = 0; i < entities.Count; i++) {
            Entity e = entities[i];
            GameObject go = e.gameObject.gameObject;
            CollisionScriptExtended script = go.GetComponent<CollisionScriptExtended>();
            if (script == null) {
                script = go.AddComponent<CollisionScriptExtended>();
            }
        }
    }
}
