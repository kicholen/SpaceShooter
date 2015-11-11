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

				// Settings stuff
				.Add(pool.CreateDifficultyControllerSystem())

	            // Initialize
				.Add(pool.CreateCreatePathSystem())
				.Add(pool.CreateCreateDifficultySystem())
				.Add(pool.CreateCreateSettingsSystem())
	            .Add(pool.CreateCreatePlayerSystem())
				.Add(pool.CreateCreateEnemySystem())
				.Add(pool.CreateWeaponSystem())
				.Add(pool.CreateCreateStaticElementsSystem())
				.Add(pool.CreateCreateCameraSystem())
				.Add(pool.CreateCreateLevelSystem())

				// Spawners
				.Add(pool.CreateSpawnMissileSystem())
				.Add(pool.CreateMultipleMissileSpawnerSystem())
				.Add(pool.CreateCircleMissileSpawnerSystem())
				.Add(pool.CreateCircleMissileRotatedSpawnerSystem())
				.Add(pool.CreateHomeMissileSpawnerSystem())
				.Add(pool.CreateFindTargetSystem())
				.Add(pool.CreateHomeMissileSystem())
				.Add(pool.CreateEnemySpawnerSystem())
				.Add(pool.CreateWaveSpawnerSystem())

			.Add(pool.CreateTimeSystem())

			// AddGO
			.Add(pool.CreateAddGameObjectSystem())
			
        	// Input
			.Add(pool.CreateCreateMouseInputSystem())
			.Add(pool.CreateProcessInputSystem())
			
				// Input player
				.Add(pool.CreatePlayerInputSystem())

				// magnet
				.Add(pool.CreateMagnetSystem())

				// AI
				.Add(pool.CreateFirstBossSystem())

			// Physics
			.Add(pool.CreateAccelerationSystem())
			.Add(pool.CreateVelocitySystem())
			.Add(pool.CreateCollisionSystem())
			.Add(pool.CreateMoveWithCameraSystem())
			.Add(pool.CreatePositionSystem())

				// Path
				.Add(pool.CreatePathSystem())

				// laser
				.Add(pool.CreateLaserSpawnerSystem())
				.Add(pool.CreateLaserSystem())

				.Add(pool.CreateRemoveOutOfViewGOSystem())
				// Physics not so much
				.Add(pool.CreateSnapPositionSystem())
				.Add(pool.CreateFaceDirectionSystem())
				.Add(pool.CreateActiveSystem())

				// Object
				.Add(pool.CreateHealthSystem())
				.Add(pool.CreateDeadPlayerSystem())
				.Add(pool.CreateAlphaSystem())

				// OnDeath Actions
				.Add(pool.CreateBonusOnDeathSystem())
				.Add(pool.CreateCameraShakeOnDeathSystem())
				.Add(pool.CreateParticlesOnDeathSystem())
				.Add(pool.CreateSoundOnDeathSystem())
				.Add(pool.CreateExplosionOnDeathSystem())

				// Particle
				.Add(pool.CreateParticleSpawnSystem())

				// bonus
				.Add(pool.CreateActivateBonusSystem())
				.Add(pool.CreateSpeedBonusSystem())

			.Add(pool.CreateRelativePositionSystem())

			// PositionGO
			.Add(pool.CreatePositionGameObjectSystem())
			// RemoveGO
			.Add(pool.CreateRemoveGameObjectSystem())
			
			// Camera
			.Add(pool.CreateRegularCameraSystem())
			.Add(pool.CreateSmoothCameraSystem())
				.Add(pool.CreateCameraShakeSystem())
			
			// Stats
			.Add(pool.CreateGameStatsSystem())
			
			// Sound
			.Add(pool.CreateSoundSystem())
			
			// Destroy
			.Add(pool.CreateDestroyEntityDelayedSystem())
			.Add(pool.CreateDestroyEntitySystem())

			.Add(pool.CreateRestartGameSystem())
			.Add(pool.CreatePauseGameSystem())
			.Add(pool.CreateSlowGameSystem());
    }
}
