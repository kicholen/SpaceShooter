using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddGameObjectSystem : IReactiveSystem {
    public TriggerOnEvent trigger { get { return Matcher.Resource.OnEntityAdded(); } }

    readonly Transform _viewContainer = new GameObject("Views").transform;

    public void Execute(List<Entity> entities) {
		Debug.Log("AddGameObjectSystem");
        for (int i = 0; i < entities.Count; i++) {
			var e = entities [i];
			var res = Resources.Load<GameObject> ("Prefab/" + e.resource.name);
			GameObject gameObject = null;
			try {
				gameObject = UnityEngine.Object.Instantiate (res);
			}
			catch (Exception) {
				Debug.Log ("Cannot instantiate " + res);
			}
			if (gameObject != null) {
				if (e.hasCollision) {
					gameObject.AddComponent<CollisionScript>();
				}
				gameObject.transform.SetParent (_viewContainer, false);
				e.AddGameObject(gameObject);
			}
		}
    }
}
