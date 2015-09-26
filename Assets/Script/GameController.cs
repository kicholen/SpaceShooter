using UnityEngine;
using Entitas;
using Entitas.Unity.VisualDebugging;

public class GameController : MonoBehaviour {
    Systems _systems;

	void Start () {
        _systems = createSystems(Pools.pool);
        _systems.Initialize();
	}

	void Update () {
        _systems.Execute();
	}

    Systems createSystems(Pool pool) {
        #if (UNITY_EDITOR)
        return new DebugSystems()
        #else
        return new Systems()
        #endif

            // Initialize
            .Add(pool.CreateCreatePlayerSystem())

        	// Input
			.Add(pool.CreateCreateInputSystem())
			.Add(pool.CreateProcessInputSystem())

			// Render
			.Add(pool.CreateAddGameObjectSystem())
			.Add(pool.CreateRemoveGameObjectSystem())

			// Position
			.Add(pool.CreatePositionSystem())

			// Destroy
			.Add(pool.CreateDestroyEntitySystem());
    }
}
