using Entitas;
using Entitas.Unity.VisualDebugging;

public class GameService : IGameService {
    Systems systems;
	Pool pool;
	Controller controller;

	public GameService(Pool pool, Controller controller) {
		this.pool = pool;
		this.controller = controller;
		systems = CreateSystems();
	}

	public void Init() {
		controller.Services.EventService.AddListener<GameEndedEvent>(onGameEnded);
		systems.Initialize();
		pool.CreateEntity()
			.AddEventService(controller.Services.EventService);
		controller.Services.Updateables.Add(this);
	}

	public void Update () {
		systems.Execute();
	}

	public void InitGame(int level) {
		pool.CreateEntity()
			.AddStartGame(level);
	}

	public void PlayGame() {
		pool.CreateEntity()
			.IsPauseGame(true);
	}

	public void InitPool(string resource, int count) {
		for (int i = 0; i < count; i++) {
			pool.CreateEntity()
				.AddResource(resource)
				.IsDestroyEntity(true);
		}
	}

	public void EndGame(Entity e) {
		e.isDestroyEntity = true;
		controller.Services.ViewService.SetView(ViewTypes.LANDING);
		pool.CreateEntity()
			.IsEndGame(true);
	}

	void onGameEnded(GameEndedEvent e) {
		pool.CreateEntity()
			.AddTween(0.0f, 2.0f, EaseTypes.Linear, 0.0f, 1.0f, 0.0f, false, null, EndGame);
	}

    Systems CreateSystems() {
        #if (UNITY_EDITOR)
        return new DebugSystems()
			.Add(pool.CreateTestSystem())
        #else
        return new Systems()
        #endif

				// Settings stuff
				.Add(pool.CreateDifficultyControllerSystem())

	            // Initialize
				.Add(pool.CreateCreateBonusSystem())
				.Add(pool.CreateCreatePathSystem())
				.Add(pool.CreateCreateDifficultySystem())
				.Add(pool.CreateCreateSettingsSystem())
	            .Add(pool.CreateCreatePlayerSystem())
				.Add(pool.CreateWeaponSystem())
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

				// blockade
				.Add(pool.CreateMovingBlockadeSystem())

			// Physics
			.Add(pool.CreateAccelerationSystem())
			.Add(pool.CreateVelocitySystem())
			.Add(pool.CreateCollisionSystem())
			.Add(pool.CreateMoveWithCameraSystem())
			.Add(pool.CreatePathSystem())
			.Add(pool.CreatePositionSystem())
			.Add(pool.CreateRotateSystem())

				// laser
				.Add(pool.CreateLaserSpawnerSystem())
				.Add(pool.CreateLaserSystem())

				// Physics not so much
				.Add(pool.CreateSnapPositionSystem())
				.Add(pool.CreateRemoveOutOfViewGOSystem())
				.Add(pool.CreateFaceDirectionSystem())
				.Add(pool.CreateActiveSystem())
				.Add(pool.CreatePlayerHealthBarSystem())

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

			// RelativePositionGO
			.Add(pool.CreateRelativePositionSystem())

			// PositionGO
			.Add(pool.CreatePositionGameObjectSystem())

			.Add(pool.CreateStartGameSystem())
			
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

			.Add(pool.CreatePauseGameSystem())
			.Add(pool.CreateEndGameSystem())
			.Add(pool.CreateSlowGameSystem())

			// Tween
			.Add(pool.CreateTweenSystem());
    }
}
