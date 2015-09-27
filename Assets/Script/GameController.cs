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
				.Add(pool.CreateCreatePlayerWeaponSystem())
				.Add(pool.CreateCreateStaticElementsSystem())

				// Spawners
				.Add(pool.CreateSpawnMissileSystem())

			// AddGO
			.Add(pool.CreateAddGameObjectSystem())
			
        	// Input
			.Add(pool.CreateCreateMouseInputSystem())
			.Add(pool.CreateProcessInputSystem())
			
				// Input player
				.Add(pool.CreatePlayerInputSystem())

			// Physics
			.Add(pool.CreateAccelerationSystem())
			.Add(pool.CreateVelocitySystem())
			.Add(pool.CreateCollisionSystem())
			.Add(pool.CreatePositionSystem())

			// PositionGO
			.Add(pool.CreatePositionGameObjectSystem())
			// RemoveGO
			.Add(pool.CreateRemoveGameObjectSystem())
			
			// Destroy
			.Add(pool.CreateDestroyEntitySystem());
    }
}
