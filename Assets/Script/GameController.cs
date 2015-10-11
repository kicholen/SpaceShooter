﻿using UnityEngine;
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
				.Add(pool.CreateCreateLevelSystem())

				// Spawners
				.Add(pool.CreateSpawnMissileSystem())
				.Add(pool.CreateCircleMissileSpawnerSystem())
				.Add(pool.CreateHomeMissileSpawnerSystem())
				.Add(pool.CreateHomeMissileSystem())
				.Add(pool.CreateEnemySpawnerSystem())

			.Add(pool.CreateTimeSystem())

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
			
				.Add(pool.CreateRemoveOutOfViewGOSystem())
				// Physics not so much
				.Add(pool.CreateSnapPositionSystem())
				.Add(pool.CreateFaceDirectionSystem())

				// Object
				.Add(pool.CreateHealthSystem())
				.Add(pool.CreateDeadPlayerSystem())
				// bonus
				.Add(pool.CreateActivateBonusSystem())
				.Add(pool.CreateSpeedBonusSystem())

			// PositionGO
			.Add(pool.CreatePositionGameObjectSystem())
			// RemoveGO
			.Add(pool.CreateRemoveGameObjectSystem())
			
			
			// Camera
			.Add(pool.CreateRegularCameraSystem())
			.Add(pool.CreateSmoothCameraSystem())
			
				// Bonus Spawner
				.Add(pool.CreateBonusSpawnerSystem())
			
			// DestroyPosition, static GO do not need to refresh on every frame
			.Add(pool.CreateDestroyPositionSystem())
			// Destroy
			.Add(pool.CreateDestroyEntitySystem())

			.Add(pool.CreateRestartGameSystem());
    }
}
