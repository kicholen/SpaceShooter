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
			.Add(pool.CreateTestSystem())
        #else
        return new Systems()
        #endif

	            // Initialize
	            .Add(pool.CreateCreatePlayerSystem())
				.Add(pool.CreateCreateEnemySystem())
				.Add(pool.CreateWeaponSystem())
				.Add(pool.CreateCreateStaticElementsSystem())
				.Add(pool.CreateCreateCameraSystem())

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
			
				// Object
				.Add(pool.CreateHealthSystem())

			// PositionGO
			.Add(pool.CreatePositionGameObjectSystem())
			// RemoveGO
			.Add(pool.CreateRemoveGameObjectSystem())
			
			// Camera
			.Add(pool.CreateRegularCameraSystem())
			.Add(pool.CreateSmoothCameraSystem())
			
			// DestroyPosition, static GO do not need to refresh on every frame
			.Add(pool.CreateDestroyPositionSystem())
			// Destroy
			.Add(pool.CreateDestroyEntitySystem());
    }
}
